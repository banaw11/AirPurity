import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Norm } from '../models/norm';
import { Sensor } from '../models/sensor';
import { StationData } from '../models/stationData';

@Injectable({
  providedIn: 'root'
})
export class SensorService {
 apiUrl = environment.apiUrl;

  private sensorsSource = new BehaviorSubject<Sensor[]>(null);
  sensors = this.sensorsSource.asObservable();

  constructor(private http: HttpClient) { }

  getSensorsData(stationId){
    return this.http.get<StationData[]>(this.apiUrl+"air?stationId="+stationId);
  }

  getNorms(){
    return this.http.get<Norm[]>(this.apiUrl+"air/norms");
  }
}
