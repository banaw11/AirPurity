import { Component, Input, OnInit, Self } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { DictionaryModel } from 'src/app/models/dictionaryModel';

@Component({
  selector: 'app-custom-dropdown',
  templateUrl: './custom-dropdown.component.html',
  styleUrls: ['./custom-dropdown.component.scss']
})
export class CustomDropdownComponent implements ControlValueAccessor,OnInit {

  @Input() label: string;
  @Input() placeholder: string;
  @Input() type = 'text';
  @Input() hideLabel = false;
  @Input() inputClass = '';
  @Input() isActive : boolean = true;
  @Input() data : BehaviorSubject<DictionaryModel[]>;
  @Input() disable = null;
  @Input() isReadOnly : boolean = false;
  tempData : {value: any, name: string}[];
  isExpanded : boolean = false;
  filteredData : {value: any, name: string, selected : boolean}[];
  selectedItem : {value: any, name: string};
  control : AbstractControl;
  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
    this.control = new FormControl();
   }
   
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }
  setDisabledState?(isDisabled: boolean): void {
  }

  ngOnInit(): void {
    this.data.subscribe(data => {
      this.filteredData = [];
      this.tempData = data;
      if(data.length > 0){
        this.filterData();
      }
    })
  }

  show(e: any){
    this.isExpanded = true;
    return false;
  }

  hide(e: any){
    this.isExpanded = false
    return false;
  }

  onSelect(index : number){
    this.toogleList();
    this.selectedItem = this.filteredData[index];
    this.filteredData.forEach(filteredItem => {
      filteredItem.selected = filteredItem == this.selectedItem;
    })
    this.ngControl.control.setValue(this.selectedItem.value);
    this.control.setValue(this.selectedItem.name);
  }

  filterData(){
    this.filteredData = [];
    let key = this.control.value;
    if(key?.length > 0){
      this.tempData.filter(item => item.name.includes(key))
      .forEach(item => {
        this.filteredData.push({
          value: item.value,
          name: item.name,
          selected: this.control.value == item.name
        })
      })
    }
    else{
      this.tempData.forEach(item => {
        this.filteredData.push({
          value: item.value,
          name: item.name,
          selected: this.control.value == item.name
        })
      })
    }   
  }

  onNgControlValueChange(value : any){
    let item = this.tempData?.find(x => x.value === value);
    if(item?.name?.length > 0 && this.control.value != item.name){
      this.control.setValue(item.name);
      this.selectedItem = item;
    }
    else if(this.control.value != value){
      this.control.setValue(value);
    }
  }

  toogleList(){
    if(this.isExpanded){
      this.hide(false);
    }
    else{
      this.show(true);
    }
  }

}
