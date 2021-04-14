import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BusyService } from 'src/app/services/busy.service';
import { CityService } from 'src/app/services/city.service';
import { OnlineClientService } from 'src/app/services/online-client.service';
import { SensorService } from 'src/app/services/sensor.service';
import { StationService } from 'src/app/services/station.service';


@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss']
})
export class CityComponent implements OnDestroy {
  sub : Subscription;
  flipped: boolean = false;
  cityName: string = null;

  constructor(private route: ActivatedRoute, public cityService: CityService, private router: Router, public stationService: StationService, public busyService: BusyService) {
   this.sub = this.route.paramMap.subscribe(params => {
      if(params.get('name')){
        this.cityName = params.get('name');
        this.cityService.getCity(this.cityName);
      }
    })
   }
   
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  loadStation(stationId: number){
    this.router.navigate(['/station'], {queryParams: {city : this.cityName, id: stationId} });
  }

  backToHome(){
    this.router.navigateByUrl('');
  }

  

}
