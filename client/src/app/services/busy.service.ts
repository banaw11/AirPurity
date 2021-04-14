import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;
  private isBusySource = new BehaviorSubject<boolean>(false);
  isBusy$ = this.isBusySource.asObservable();

  constructor(private spinnerService: NgxSpinnerService) { }

  busy(){
    this.isBusySource.next(true);
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'ball-spin-clockwise',
      size:"large",
      bdColor: '#29157a;',
      color: "#ece3ff"
    })
  }

  idle(){
    this.busyRequestCount--;
    if(this.busyRequestCount <=0){
      this.busyRequestCount = 0;
      this.spinnerService.hide();
      this.isBusySource.next(false);
    }
  }
}
