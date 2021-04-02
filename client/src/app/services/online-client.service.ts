import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {  HttpRequest, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OnlineClientService {

  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;

  constructor(private http: HttpClient) { }


  createHubConnection(stationId: number){
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl + 'online?stationId=' +stationId)
    .withAutomaticReconnect()
    .build()

    this.hubConnection.start()
  }
}
