import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NbMenuItem, NbMenuService } from '@nebular/theme';
import { filter, map } from 'rxjs/operators';
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
  items: NbMenuItem[] = [{title:"brak wyników"}];
  itemData: {city:CityDTO, commune: string, district: string, province: string} = null;

  constructor(private fb: FormBuilder, private menuService: NbMenuService) { 
    this.cityForm = this.fb.group({
      provinceControl: [{value: null}],
      districtControl: [{value: null, disabled: true}],
      communeControl: [{value: null, disabled: true}],
      cityControl: [{value: null, disabled: true}]
    })
  }

  ngOnInit(): void {
    this.menuService.onItemClick()
    .pipe(filter(({ tag }) => tag === 'city-menu'),
    map(({ item: { data } }) => {
      this.districts = [];
      this.communes = [];
      this.cities = [];
      this.cityForm.controls.provinceControl.setValue(data.province);
      this.cityForm.controls.districtControl.setValue(data.district);
      this.cityForm.controls.communeControl.setValue(data.commune);
      this.cityForm.controls.cityControl.setValue(data.city.name);

      
    }),).subscribe();

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

  }

  onKeyUp(event: any){
    if(event.target.value.length > 0){
      this.items = [];
    this.provinces.forEach(x => x.districts.forEach(y => y.communes.forEach(z => z.cities.forEach(c => {
      if(c.name.toLowerCase().startsWith(event.target.value.toLowerCase())){
        this.items.push({title:c.name, data:{city:c, commune:z.name, district:y.name, province:x.name}})
      }
     }))));
     if(this.items.length < 1){
      this.items = [{title:"brak wyników"}];
     }
    }
    else{
      this.items = [{title:"brak wyników"}];
    }
    
  }



}
