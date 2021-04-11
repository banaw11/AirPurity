import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Sensor } from '../models/sensor';
import { Station } from '../models/station';
import { StationState } from '../models/stationState';
import { CityService } from './city.service';


@Injectable({
  providedIn: 'root'
})
export class StationService {
  apiUrl = environment.apiUrl;
  curentStationId: number = null;

  private stationSource = new BehaviorSubject<Station>(null);
  station$ = this.stationSource.asObservable();

  constructor(private http: HttpClient, private cityService: CityService) { }

  getStation(stationId: number){
    this.http.get<Station>(this.apiUrl+'station?stationId='+stationId).subscribe((station: Station) => {
      if(station){
        this.loadStation(station);
      }
    })
  }

  loadStation(station: Station){
    this.getSensors(station.id).subscribe(sensors => {
      station.sensors = sensors;
    });
    this.getStationState(station.id).subscribe(state => {
      station.stationState = state;
    })
    this.stationSource.next(station);
  }

  getSensors(stationId: number){
    this.curentStationId = stationId;
    return this.http.get<Sensor[]>(this.apiUrl+'station/sensors?stationId='+stationId);
  }

  getStationState(stationId: number){
    return this.http.get<StationState>(this.apiUrl+'air/quality?stationId='+stationId);
  }

  updateStationState(state: StationState){
    let tempStation: Station = this.stationSource.value;
    tempStation.stationState = state;
    this.stationSource.next(tempStation);
  }

  

}
