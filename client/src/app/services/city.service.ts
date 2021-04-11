import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { City } from '../models/city';
import { ProvinceDTO } from '../models/formDTOs/provinceDTO';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  apiUrl = environment.apiUrl;

  private citiesFormSource = new BehaviorSubject<ProvinceDTO[]>([]);
  citiesForm$ = this.citiesFormSource.asObservable();

  private citySource = new BehaviorSubject<City>(null);
  city$ = this.citySource.asObservable();


  constructor(private http: HttpClient) { }

  getCities(){
    if(this.citiesFormSource.value.length < 1){
      this.http.get(this.apiUrl + 'city/all').subscribe((response: ProvinceDTO[]) => {
        this.citiesFormSource.next(response);
      })
    }
    
  }

  getCity(cityName: string) {  
    this.http.get(this.apiUrl + 'city?cityName='+cityName).subscribe((response: City) => {
    this.citySource.next(response);
  });
  
  }
}


