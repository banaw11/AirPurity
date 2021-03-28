import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { StationData } from '../models/stationData';

@Injectable({
  providedIn: 'root'
})
export class SensorService {
 apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getSensorsData(stationId){
    return this.http.get<StationData[]>(this.apiUrl+"air?stationId="+stationId);
  }
}
