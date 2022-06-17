import { Component, EventEmitter, Input, OnChanges, OnInit, Output, Self, SimpleChanges } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { DictionaryModel } from 'src/app/models/dictionaryModel';

@Component({
  selector: 'app-custom-dropdown',
  templateUrl: './custom-dropdown.component.html',
  styleUrls: ['./custom-dropdown.component.scss']
})
export class CustomDropdownComponent implements ControlValueAccessor, OnInit {

  @Input() label: string;
  @Input() placeholder: string;
  @Input() type = 'text';
  @Input() hideLabel = false;
  @Input() inputClass = '';
  @Input() isActive : boolean = true;
  @Input() data = new BehaviorSubject<DictionaryModel[]>([])
  @Input() disable = null;
  @Input() isReadOnly : boolean = true;
  @Input() isFilterable : boolean = false;
  tempData : {value: any, name: string}[];
  isExpanded : boolean = false;
  filteredData : {value: any, name: string, selected : boolean}[];
  selectedItem : {value: any, name: string};
  control : AbstractControl = new FormControl();
  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
    
   }
  ngOnInit(): void {
    this.data.subscribe(data => {
      this.filteredData = [];
      this.tempData = data;
      if(data?.length > 0){
        this.filterData();
      }
      this.selectedItem = null;
    })

    this.onNgControlValueChange(this.ngControl.control.value);
    this.ngControl.control.valueChanges.subscribe(value => {
      this.onNgControlValueChange(value);
    })
    this.control.valueChanges.subscribe(value => {
      if(this.tempData?.length > 0 && this.isFilterable){
        this.filterData();
      }
    })
    
  }
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }
  setDisabledState?(isDisabled: boolean): void {
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
    this.control.setValue(this.selectedItem.name);
    this.ngControl.control.setValue(this.selectedItem.value);
  }

  filterData(){
    this.filteredData = [];
    let key = this.control.value;
    if(key?.length > 0 && this.isFilterable){
      this.tempData.filter(item => item.name.toLocaleLowerCase().includes(key.toLocaleLowerCase()))
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
    else if(value == null){
      this.control.reset(null);
      this.selectedItem = null;
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
