import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CityDTO } from 'src/app/models/formDTOs/cityDTO';
import { CommuneDTO } from 'src/app/models/formDTOs/communeDTO';
import { DistrictDTO } from 'src/app/models/formDTOs/districtDTO';
import { ProvinceDTO } from 'src/app/models/formDTOs/provinceDTO';
import { CityService } from 'src/app/services/city.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  cityForm: FormGroup;
  
  provinces: ProvinceDTO[] = [];
  districts: DistrictDTO[] = [];
  communes: CommuneDTO[] =[];
  cities: CityDTO[] = [];


  constructor(private fb: FormBuilder, private cityService: CityService, private router: Router) { 
    this.cityService.getCities();
    this.cityForm = this.fb.group({
      provinceControl: [{value: null}],
      districtControl: [{value: null, disabled: true}],
      communeControl: [{value: null, disabled: true}],
      cityControl: [{value: null, disabled: true}]
    })
  }

  ngOnInit(): void {
    this.cityService.citiesForm$.subscribe(proviences => this.provinces=proviences);
    this.cityForm.get('provinceControl').valueChanges.subscribe(x => x != null ? this.provinceSelected(x): null);
    this.cityForm.get('districtControl').valueChanges.subscribe(x => x != null ? this.districtSelected(x): null);
    this.cityForm.get('communeControl').valueChanges.subscribe(x => x != null ? this.communeSelected(x): null);
  }

  provinceSelected(event: any){
    this.districts = this.provinces.find(x => x.name === event).districts;
    this.cityForm.controls.districtControl.setValue(null);
    this.cityForm.controls.communeControl.setValue(null);
    this.cityForm.controls.cityControl.setValue(null);
    if(event != null){
      this.cityForm.controls.districtControl.enable();
    }
    else{
      this.cityForm.controls.districtControl.disable();
    }
    this.cityForm.controls.communeControl.disable();
    this.cityForm.controls.cityControl.disable();
    
  }
  districtSelected(event: any){
    this.communes = this.provinces.find(x => x.name === this.cityForm.controls.provinceControl.value)
      .districts.find(x => x.name === event).communes;
    this.cityForm.controls.communeControl.setValue(null);
    this.cityForm.controls.cityControl.setValue(null);
    if(event != null){
      this.cityForm.controls.communeControl.enable();
    }
    else{
      this.cityForm.controls.communeControl.disable();
    }
    this.cityForm.controls.cityControl.disable();
    
  }
  communeSelected(event: any){
    this.cities = this.provinces.find(x => x.name === this.cityForm.controls.provinceControl.value).districts.find(
      x => x.name === this.cityForm.controls.districtControl.value)
        .communes.find(x => x.name === event).cities;
    this.cityForm.controls.cityControl.setValue(null);
    if(event != null){
      this.cityForm.controls.cityControl.enable();
    }
    else{
      this.cityForm.controls.cityControl.disable();
    }
  }

  citySelected(){
    this.cityService.getCity(this.cityForm.controls.cityControl.value);
    this.router.navigateByUrl("/city")
  }


}
