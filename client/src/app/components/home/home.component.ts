import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CityDTO } from 'src/app/models/formDTOs/cityDTO';
import { CommuneDTO } from 'src/app/models/formDTOs/communeDTO';
import { DistrictDTO } from 'src/app/models/formDTOs/districtDTO';
import { ProvinceDTO } from 'src/app/models/formDTOs/provinceDTO';
import { CityQuery } from 'src/app/models/QueryParams/city-query';
import { BusyService } from 'src/app/services/busy.service';
import { CityService } from 'src/app/services/city.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  cityForm: FormGroup;
  
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


  constructor(private fb: FormBuilder, private cityService: CityService, private router: Router, public busyService: BusyService) { 
    this.cityService.getCities(this.cityQuery);
    this.cityForm = this.fb.group({
      provinceControl: [{value: null}],
      districtControl: [{value: null, disabled: true}],
      communeControl: [{value: null, disabled: true}],
      cityControl: [{value: null, disabled: true}]
    })
  }

  ngOnInit(): void {
    this.cityService.citiesForm$.subscribe(provinces => {
      this.provinces = provinces;
      if(provinces.length>0){
        provinces.forEach(p => {
          if(p.provinceName === this.cityQuery.provinceName){
            this.districts = p.districts;
            p.districts.forEach(d => {
              if(d.districtName === this.cityQuery.districtName){
                this.communes = d.communes;
                d.communes.forEach(c => {
                  if(c.communeName === this.cityQuery.communeName )
                  {
                    this.cities = c.cities;
                  }
                })
              }
            })
          }
        })
        
      }
      
    });
    this.cityForm.get('provinceControl').valueChanges.subscribe(x => x != null ? this.provinceSelected(x): null);
    this.cityForm.get('districtControl').valueChanges.subscribe(x => x != null ? this.districtSelected(x): null);
    this.cityForm.get('communeControl').valueChanges.subscribe(x => x != null ? this.communeSelected(x): null);
  }

  provinceSelected(event: any){
    this.cityQuery.provinceName = event;
    this.cityService.getCities(this.cityQuery);

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
    this.cityQuery.districtName = event;
    this.cityService.getCities(this.cityQuery);

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
    this.cityQuery.communeName = event;
    this.cityService.getCities(this.cityQuery);
    
    this.cityForm.controls.cityControl.setValue(null);
    if(event != null){
      this.cityForm.controls.cityControl.enable();
    }
    else{
      this.cityForm.controls.cityControl.disable();
    }
  }


  citySelected(){
    this.router.navigateByUrl("/city/"+this.cityForm.controls.cityControl.value)
  }





}
