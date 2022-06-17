import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { DictionaryModel } from 'src/app/models/dictionaryModel';
import { CityDTO } from 'src/app/models/formDTOs/cityDTO';
import { CommuneDTO } from 'src/app/models/formDTOs/communeDTO';
import { DistrictDTO } from 'src/app/models/formDTOs/districtDTO';
import { ProvinceDTO } from 'src/app/models/formDTOs/provinceDTO';
import { CityQuery } from 'src/app/models/QueryParams/city-query';
import { ResponseModel } from 'src/app/models/responseModel';
import { BusyService } from 'src/app/services/busy.service';
import { CityService } from 'src/app/services/city.service';
import { DictionaryService } from 'src/app/services/dictionary.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  cityForm: FormGroup;
  
  provincesDictionary = new BehaviorSubject<DictionaryModel[]>([]);
  districtsDictionary = new BehaviorSubject<DictionaryModel[]>([]);
  communesDictionary = new BehaviorSubject<DictionaryModel[]>([]);
  citiesDictionary = new BehaviorSubject<DictionaryModel[]>([]);
  provinces: ProvinceDTO[] = [];
  provience: ProvinceDTO = null;
  districts: DistrictDTO[] = [];
  communes: CommuneDTO[] =[];
  cities: CityDTO[] = [];
  cityQuery: CityQuery ={ 
    provinceName: null,
    districtName: null,
    communeName:null
  }

  apiUrl = environment.apiUrl;

  constructor(private fb: FormBuilder, private http : HttpClient, private router: Router, public busyService: BusyService, private dictionaryService : DictionaryService) { 
    this.cityForm = this.fb.group({
      province: new FormControl(),
      district: new FormControl({disabled: true}),
      commune: new FormControl({disabled: true}),
      city : new FormControl({disabled: true})
    })
    this.dictionaryService.getProvinces().subscribe(dict => {
        this.provincesDictionary.next(dict);
    });
  }

  ngOnInit(): void {
    this.cityForm.get('province').valueChanges.subscribe(x => this.isProvinceValid() ? this.provinceSelected(x): null);
    this.cityForm.get('district').valueChanges.subscribe(x => this.isDistrictValid() ? this.districtSelected(x): null);
    this.cityForm.get('commune').valueChanges.subscribe(x => this.isCommuneValid() ? this.communeSelected(x): null);
  }

  provinceSelected(value: any){
    this.dictionaryService.getDistricts(value).subscribe(dict => {
      this.districtsDictionary.next(dict);
    })
    this.cityForm.controls["district"].setValue(null);
    this.cityForm.controls["commune"].setValue(null);
    this.cityForm.controls["city"].setValue(null);
    this.communesDictionary.next([]);
    this.citiesDictionary.next([]);
  }

  districtSelected(value: any){
    this.dictionaryService.getCommunes(value).subscribe(dict => {
      this.communesDictionary.next(dict);
    })
    this.cityForm.controls["commune"].setValue(null);
    this.cityForm.controls["city"].setValue(null);
    this.citiesDictionary.next([]);
  }

  communeSelected(value: any){
    this.dictionaryService.getCities(value).subscribe(dict => {
      this.citiesDictionary.next(dict);
    })
  }

  citySelected(){
    let current = this.cityForm.controls["city"].value;
    let index = this.citiesDictionary.value.findIndex(x => x.value == current)
    if(index > -1){
      let cityName = this.citiesDictionary.value[index].name;
      this.router.navigateByUrl("/city/"+cityName);
    }
  }

  isProvinceValid(){
    let current = this.cityForm.controls["province"].value;
    return this.provincesDictionary.value.findIndex(x => x.value == current) > -1
  }

  isDistrictValid(){
    let current = this.cityForm.controls["district"].value;
    return this.districtsDictionary.value.findIndex(x => x.value == current) > -1 
  }

  isCommuneValid(){
    let current = this.cityForm.controls["commune"].value;
    return this.communesDictionary.value.findIndex(x => x.value == current) > -1 
  }

  isCityValid(){
    let current = this.cityForm.controls["city"].value;
    return this.citiesDictionary.value.findIndex(x => x.value == current) > -1 
  }



}
