import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-custom-input',
  templateUrl: './custom-input.component.html',
  styleUrls: ['./custom-input.component.scss']
})
export class CustomInputComponent implements ControlValueAccessor, OnInit {
  @Input() label: string;
  @Input() placeholder: string;
  @Input() type = 'text';
  @Input() hideLabel = false;
  @Input() inputClass = '';
  @Input() removable = false;
  @Input() groupIndex : number;
  @Input() removeEvent : (index: number) => void;
  @Input() disable = null;
  @Input() isReadOnly : boolean = false;

  tooglePassword : boolean = false;
  inputType : string;
  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
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
    this.inputType = this.type;
  }

  remove(){
    if(this.removeEvent){
      this.removeEvent(this.groupIndex);
    }
  }

  changeType(){
    this.tooglePassword = !this.tooglePassword;
    this.inputType = this.tooglePassword ? "text" : "password";
  }

}
