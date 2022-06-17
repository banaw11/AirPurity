import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BusyService } from 'src/app/services/busy.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-stop-notification',
  templateUrl: './stop-notification.component.html',
  styleUrls: ['./stop-notification.component.scss']
})
export class StopNotificationComponent implements OnInit {
  sub : Subscription;

  constructor(private route: ActivatedRoute, private router: Router, private notificationService : NotificationService, public busyService: BusyService) {
    if(this.route.snapshot.queryParamMap.has("token")){
      let token = this.route.snapshot.queryParamMap.get('token');
      this.notificationService.stopNotification(token);
    }
    
   }

  ngOnInit(): void {
  }

  backToHome(){
    this.router.navigateByUrl('');
  }

}
