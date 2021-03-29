import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Sensor } from '../models/sensor';
import { Station } from '../models/station';
import { StationState } from '../models/stationState';
import { CityService } from './city.service';
import { SensorService } from './sensor.service';

@Injectable({
  providedIn: 'root'
})
export class StationService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getSensors(stationId: number){
    return this.http.get<Sensor[]>(this.apiUrl+'station/sensors?stationId='+stationId);
  }

  getStationState(stationId: number){
    return this.http.get<StationState>(this.apiUrl+'air/quality?stationId='+stationId);
  }
}
