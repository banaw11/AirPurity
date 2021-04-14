import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Measure } from '../models/measure';
import { Norm } from '../models/norm';
import { Sensor } from '../models/sensor';
import { StationData } from '../models/stationData';
import { StationService } from './station.service';

@Injectable({
  providedIn: 'root'
})
export class SensorService {
 apiUrl = environment.apiUrl;

  private stationDataSource = new BehaviorSubject<StationData[]>([]);
  stationData$ = this.stationDataSource.asObservable();

  private normsSource = new BehaviorSubject<Norm[]>([]);
  norms$ = this.normsSource.asObservable();
 
  private pmMeasuresSource = new BehaviorSubject<{pm10: Measure[], pm25: Measure[]}>({pm10:[],pm25:[]});
  pmMeasures$ = this.pmMeasuresSource.asObservable();



  constructor(private http: HttpClient) {
    this.stationData$.subscribe( stationData => {
      if(stationData){
        this.pmMeasuresSource.next(
          {
            pm10: this.getPM10Data(),
            pm25: this.getPM25Data()
          }
          )
      }
    })
   }

  getSensorsData(stationId){
    this.getNorms();
    this.http.get<StationData[]>(this.apiUrl+"air?stationId="+stationId).subscribe((stationData: StationData[]) => {
     this.updateStationData(stationData);
   })
  }

  getNorms(){
    this.http.get<Norm[]>(this.apiUrl+"air/norms").subscribe((norms: Norm[]) => {
      this.normsSource.next(norms);
    })
  }

  updateStationData(stationData: StationData[]){
    stationData.forEach( x => {
      if(x.values[0]){
        x.percents = this.getPercentsNorm(x.paramCode, x.values[0].value);
      }
    })
    this.stationDataSource.next(stationData);
  }

  getPercentsNorm(paramCode: string, value: number): number{
    return value / this.normsSource.value.find(x => x.paramCode == paramCode).paramNorm * 100;
  }

  getNorm(paramCode: string): number{
    return this.normsSource.value.find(x => x.paramCode == paramCode).paramNorm;
  }

  getPM10Data():Measure[]{
    return this.stationDataSource.value.find(x => x.paramCode == "PM10") ? this.stationDataSource.value.find(x => x.paramCode == "PM10").values : [];
  }

  getPM25Data():Measure[]{
    return this.stationDataSource.value.find(x => x.paramCode == "PM2.5") ? this.stationDataSource.value.find(x => x.paramCode == "PM2.5").values : [];
  }
}
