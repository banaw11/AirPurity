import { HttpClient } from '@angular/common/http';
import { ComponentFactoryResolver, ComponentRef, Injectable, ViewContainerRef } from '@angular/core';
import { Subject, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { NotificationComponent } from '../components/notification/notification.component';
import { Notification } from '../models/notification';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private resolver: ComponentFactoryResolver) { }

  stopNotification(token: string){
    this.http.post(this.apiUrl + "Notification/stop?token=" + token, {}).subscribe();
  }

  confirmEmail(token: string){
    return this.http.post(this.apiUrl + "Notification/email-confirmation?token=" + token, {}).pipe(
      map((response : ResponseModel) => {
          return response;
      })
      ,catchError(err => throwError(err))
    )
  }

   createNotification(model: Notification){
    return this.http.post(this.apiUrl + "Notification/create", model)
      .pipe(
        map((response : ResponseModel) => {
            return response;
        })
        ,catchError(err => throwError(err))
      )
   }
}
