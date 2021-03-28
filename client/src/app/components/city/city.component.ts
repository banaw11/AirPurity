import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { StationData } from 'src/app/models/stationData';
import { CityService } from 'src/app/services/city.service';
import { SensorService } from 'src/app/services/sensor.service';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss']
})
export class CityComponent implements OnInit {

  private sensorsDataSource = new BehaviorSubject<StationData[]>(null);
  sensorData$ = this.sensorsDataSource.asObservable();

  constructor(public cityService: CityService, private sensorService: SensorService) {
    this.cityService.getCity("Poznań");
   }

  ngOnInit(): void {
  }

  loadSensors(stationId : number){
    this.cityService.loadSensors(stationId);
  }

  loadSensorsData(stationId: number){
    this.sensorService.getSensorsData(stationId).subscribe((stationsData: StationData[]) => {
      this.sensorsDataSource.next(stationsData);
    })
  }



}
