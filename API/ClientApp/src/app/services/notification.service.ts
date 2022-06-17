import { HttpClient } from '@angular/common/http';
import { ComponentFactoryResolver, ComponentRef, Injectable, ViewContainerRef } from '@angular/core';
import { Subject, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { NotificationComponent } from '../components/notification/notification.component';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  apiUrl = environment.apiUrl;
  private cmpRef!: ComponentRef<NotificationComponent>;
  private cmpSubscriber!: Subject<boolean>;

  constructor(private http: HttpClient, private resolver: ComponentFactoryResolver) { }

  stopNotification(email: string){
    this.http.post(this.apiUrl + "Notification/delete?email=" + email, {}).subscribe();
  }

  showCreateNotificationModal(entry: ViewContainerRef){
    let factory = this.resolver.resolveComponentFactory(NotificationComponent);
    this.cmpRef = entry.createComponent(factory);
    this.cmpRef.instance.closeEvent.subscribe(() => this.closeModal());
    this.cmpSubscriber = new Subject<boolean>();
    return this.cmpSubscriber.asObservable();
  }

  private closeModal(){
    this.cmpSubscriber.next(false);
    this.cmpSubscriber.complete();
    this.cmpRef.destroy();
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