import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { DictionaryModel } from 'src/app/models/dictionaryModel';
import { BusyService } from 'src/app/services/busy.service';
import { DictionaryService } from 'src/app/services/dictionary.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SensorService } from 'src/app/services/sensor.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {
  indexLevelDictionary = new BehaviorSubject<DictionaryModel[]>([]);
  paramDictionary = new BehaviorSubject<DictionaryModel[]>([]);
  notificationForm: FormGroup;

  @Output() closeEvent = new EventEmitter();
  @Input() cityId : number = 0;
  @Input() stationId : number = 0;
  @HostListener('document:keydown.escape', ['$event']) onKeydownHandler(event: KeyboardEvent) {
    this.close();
  }

  constructor(private router : Router, private notificationService : NotificationService, public busyService : BusyService, private fb : FormBuilder,
    private dictionaryService : DictionaryService, private sensorService : SensorService) {
      this.dictionaryService.getIndexLevels().subscribe(dict => {
        this.indexLevelDictionary.next(dict);
      });

      this.sensorService.getParams().subscribe(dict => {
        this.paramDictionary.next(dict);
      })
   }

  ngOnInit(): void {
    let params = this.paramDictionary.getValue();
    this.notificationForm = this.fb.group({
      userEmail: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]],
      cityId: [this.cityId, Validators.required],
      stationId: [this.stationId, Validators.required],
      indexLevelId: new FormControl(),
      notificationSubjects: params.length > 0 ? this.fb.array([
        this.fb.group({
          paramCode : [params[0].value],
          indexLevelId: [0],
          index : 0
        })
      ]) : []
    })
  }


  backToHome(){
    this.router.navigateByUrl('');
  }

  close(){
    this.closeEvent.emit();
  }

  create(){
    let model = new Notification(this.notificationForm.value);
    if(model){
      this.notificationService.createNotification(model).subscribe(res => {
        if(res.success){
          console.log(res.message);
        }
      })
    }
  }

  addSubject(){
    let params = this.paramDictionary.getValue();
    var control = <FormArray>this.notificationForm.controls['notificationSubjects'];
    if(control.controls.length < params.length){
      let index = this.getNextFormGroupIndex(<FormGroup[]>control.controls);
      control.push(this.fb.group({
        paramCode : [params[0].value],
        indexLevelId: [0],
        index : index
      }));
    }
  }

  private getNextFormGroupIndex(controls : FormGroup[]) : number{
    if(controls.length > 1){
      for (let i = 0; i < controls.length - 1; i++) {
        if(controls[i+1].controls["index"].value - controls[i].controls["index"].value > 1){
          return controls[i].controls["index"].value + 1
        }
      }
    }
    else if(controls.length == 1){
      return controls[0].controls["index"].value == 0 ? 1 : 0;
    }
    return controls.length;
  }

  removeControlSubject = (index : number)=> {
    let controls = <FormArray>this.notificationForm.controls['notificationSubjects'];
    let groupIndex = controls.controls.findIndex((group : FormGroup) => {
      return group.controls["index"].value == index;
    })
    if(groupIndex > -1){
      controls.removeAt(groupIndex)
    }
    this.notificationForm.updateValueAndValidity();
    controls.length == 0 && this.addSubject();
  }

}
