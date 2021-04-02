import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map, take} from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { City } from '../models/city';
import { Sensor } from '../models/sensor';
import { Station } from '../models/station';
import { StationState } from '../models/stationState';
import { OnlineClientService } from './online-client.service';
import { StationService } from './station.service';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  apiUrl = environment.apiUrl;

  private citySource = new BehaviorSubject<City>(null);
  city$ = this.citySource.asObservable();

  constructor(private http: HttpClient, private stationService: StationService, private onlineClientService: OnlineClientService) { }

  getCity(cityName: string) {  
    this.http.get(this.apiUrl + 'city?cityName='+cityName).subscribe((response: City) => {
    this.citySource.next(response);
  });
  
  }

  loadSensors(stationId : number){
    let temp: City = this.citySource.value;
    this.stationService.getSensors(stationId).subscribe((sensors: Sensor[]) => {
      temp.stations.find(x => x.id == stationId).sensors= sensors;
    })
    this.citySource.next(temp);
    this.loadStationState(stationId);
    this.onlineClientService.createHubConnection(stationId);
  }

  loadStationState(stationId: number){
    let temp: City = this.citySource.value;
    this.stationService.getStationState(stationId).subscribe((stationState: StationState) => {
      temp.stations.find(x => x.id == stationId).stationState = stationState;
    })
    this.citySource.next(temp);
  }
}


