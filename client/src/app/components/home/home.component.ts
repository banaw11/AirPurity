import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CityDTO } from 'src/app/models/formDTOs/cityDTO';
import { CommuneDTO } from 'src/app/models/formDTOs/communeDTO';
import { DistrictDTO } from 'src/app/models/formDTOs/districtDTO';
import { ProvinceDTO } from 'src/app/models/formDTOs/provinceDTO';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  cityForm: FormGroup;

  provinces: ProvinceDTO[] = [
    {name: "WIELKOPOLSKIE", 
    districts: [
      {name: "poznanski", communes: [
        {name: "poznan", cities: [
          {name:"Poznań", id: 1}
        ]},
        {name:"luboń", cities: [
          {name:"Luboń", id:2}
        ]}
      ]}
    ]},
    {name: "ZACHODNIOPOMORSKIE", 
    districts: [
      {name: "GRYFIŃSKI", communes: [
        {name: "CEDYNIA", cities: [
          {name:"Cedynia", id: 11},
          {name:"Osinów-Dolny", id: 12}
        ]},
        {name:"Chojna", cities: [
          {name:"Chojna", id:21}
        ]}
      ]},
      {name: "Szczeciński", communes: [
        {name: "Szczecin", cities: [
          {name:"Szczecin", id: 31}
        ]}
      ]}
    ]},
  ]

  districts: DistrictDTO[] = [];
  communes: CommuneDTO[] =[];
  cities: CityDTO[] = [];

  constructor(private fb: FormBuilder) { 
    this.cityForm = this.fb.group({
      provinceControl: [{value: null}],
      districtControl: [{value: null, disabled: true}],
      communeControl: [{value: null, disabled: true}],
      cityControl: [{value: null, disabled: true}]
    })
  }

  ngOnInit(): void {
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
    this.communes = this.provinces.find(x => x.name === this.cityForm.controls.provinceControl.value).districts.find(x => x.name === event).communes;
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
      x => x.name === this.cityForm.controls.districtControl.value).communes.find(x => x.name === event).cities;
    this.cityForm.controls.cityControl.setValue(null);
    if(event != null){
      this.cityForm.controls.cityControl.enable();
    }
    else{
      this.cityForm.controls.cityControl.disable();
    }
  }

  citySelected(){

  }



}
