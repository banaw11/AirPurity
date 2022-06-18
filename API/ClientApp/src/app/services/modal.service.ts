import { ComponentFactoryResolver, ComponentRef, Injectable, ViewContainerRef } from '@angular/core';
import { Subject } from 'rxjs';
import { NotificationComponent } from '../components/notification/notification.component';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private cmpRef!: ComponentRef<NotificationComponent>;
  private cmpSubscriber!: Subject<boolean>;
  constructor(private resolver: ComponentFactoryResolver) { }

  showCreateNotificationModal(entry: ViewContainerRef, stationId: number){
    let factory = this.resolver.resolveComponentFactory(NotificationComponent);
    this.cmpRef = entry.createComponent(factory);
    this.cmpRef.instance.stationId = stationId;
    this.cmpRef.instance.closeEvent.subscribe(() => this.closeModal());
    this.cmpSubscriber = new Subject<boolean>();
    return this.cmpSubscriber.asObservable();
  }

  private closeModal(){
    this.cmpSubscriber.next(false);
    this.cmpSubscriber.complete();
    this.cmpRef.destroy();
   }
}
