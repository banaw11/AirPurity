import { Component } from '@angular/core';
import { CityService } from './services/city.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'AirPurity';
  constructor(private cityService: CityService){
    //this.cityService.getCity("Poznań");
  }
}
