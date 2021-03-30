import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { take } from 'rxjs/operators';
import { City } from 'src/app/models/city';
import { Measure } from 'src/app/models/measure';
import { Norm } from 'src/app/models/norm';
import { Station } from 'src/app/models/station';
import { StationData } from 'src/app/models/stationData';
import { CityService } from 'src/app/services/city.service';
import { SensorService } from 'src/app/services/sensor.service';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss']
})
export class CityComponent implements OnInit {
  flipped: boolean = false;
  stationsData : StationData[];
  norms: Norm[];
  station: Station;

  private stationsSource = new BehaviorSubject<Station[]>(null);
  stations$ = this.stationsSource.asObservable();

  constructor(public cityService: CityService, private sensorService: SensorService) {
    this.cityService.city$.pipe().subscribe((city: City) => {
      if(city){
        this.stationsSource.next(city.stations);
      }
      
    })
    this.sensorService.getNorms().subscribe((norms : Norm[]) => {
      this.norms = norms;
    })
   }

  ngOnInit(): void {
    
  }

  loadSensors(stationId : number){
    this.cityService.loadSensors(stationId);
    this.loadSensorsData(stationId);
    this.stations$.subscribe(station => {
      this.station = station.find(x => x.id == stationId);
    })
  }

  loadSensorsData(stationId: number){
    this.sensorService.getSensorsData(stationId).subscribe((stationsData: StationData[]) => {
      stationsData.forEach( x => {
        if(x.values[0]){
          x.percents = this.getPercentsNorm(x.paramCode, x.values[0].value);
        }
      })
      this.stationsData = stationsData;
      this.flipped = true;
    })
  }

  getPercentsNorm(paramCode: string, value: number): number{
    return value / this.norms.find(x => x.paramCode == paramCode).paramNorm * 100;
  }

  getNorm(paramCode: string): number{
    return this.norms.find(x => x.paramCode == paramCode).paramNorm;
  }

  getStationState(): string{
    return this.station.stationState.stIndexLevel.indexLevelName;
  }

  getIndexLevel(): number{
    return this.station.stationState.stIndexLevel.id;
  }

  getPM10Data():Measure[]{
    return this.stationsData.find(x => x.paramCode == "PM10").values;
  }

  getPM25Data():Measure[]{
    return this.stationsData.find(x => x.paramCode == "PM2.5").values;
  }

}
