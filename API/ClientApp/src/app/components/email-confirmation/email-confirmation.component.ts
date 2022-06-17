import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BusyService } from 'src/app/services/busy.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.scss']
})
export class EmailConfirmationComponent implements OnInit {
  responseMsg: string ="";

  constructor(private route: ActivatedRoute, private router: Router, private notificationService : NotificationService, public busyService: BusyService) {
    if(this.route.snapshot.queryParamMap.has("token")){
      let token = this.route.snapshot.queryParamMap.get('token');
      
      this.notificationService.confirmEmail(token).subscribe(res => {
        this.responseMsg = res.message;
      })
    }
   }

  ngOnInit(): void {
  }

  backToHome(){
    this.router.navigateByUrl('');
  }
}
