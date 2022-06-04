import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {  HttpRequest, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { StationData } from '../models/stationData';
import { StationState } from '../models/stationState';
import { CityService } from './city.service';
import { SensorService } from './sensor.service';
import { StationService } from './station.service';

@Injectable({
  providedIn: 'root'
})
export class OnlineClientService {

  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;

  constructor(private http: HttpClient, private sensorService: SensorService, private stationService: StationService, private cityService: CityService) { }


  createHubConnection(stationId: number){
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl + 'online?stationId=' +stationId)
    .withAutomaticReconnect()
    .build()

    this.hubConnection.start()

    this.hubConnection.on('RefreshedAirData', (data: StationData[]) => {
      this.sensorService.updateStationData(data);
    })

    this.hubConnection.on('RefreshedAirQuality', (state: StationState) => {
      this.stationService.updateStationState(state);
    })
  }

  stopHubConnection() {
    this.hubConnection.stop().catch(error => console.log(error));
  }
}
