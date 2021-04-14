import { Component, OnDestroy, OnInit, } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BusyService } from 'src/app/services/busy.service';
import { OnlineClientService } from 'src/app/services/online-client.service';
import { SensorService } from 'src/app/services/sensor.service';
import { StationService } from 'src/app/services/station.service';

@Component({
  selector: 'app-station',
  templateUrl: './station.component.html',
  styleUrls: ['./station.component.scss']
})
export class StationComponent implements OnInit, OnDestroy {
  sub: Subscription;
  stationId : number
  cityName: string;

  constructor(public stationService: StationService, public sensorService: SensorService, private onlineService: OnlineClientService, private router: Router,
     private route: ActivatedRoute, public busyService: BusyService) {
      this.sub = this.route.queryParams.subscribe(params => {
         this.stationId = + params['id'];
         this.cityName =  params['city'];
       })
   }
  ngOnDestroy(): void {
    this.onlineService.stopHubConnection();
    this.sub.unsubscribe();
  }
  ngOnInit(): void {
    this.onlineService.createHubConnection(this.stationId);
    this.stationService.getStation(this.stationId);
    this.sensorService.getSensorsData(this.stationId);
  }
 

  backToCity(){
    this.onlineService.stopHubConnection();
    this.router.navigateByUrl('/city/'+this.cityName);
  }

}
