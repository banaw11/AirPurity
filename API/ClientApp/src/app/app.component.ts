import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { CityService } from './services/city.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'AirPurity';
  @ViewChild('wraper', { read: ViewContainerRef })
  wraper!: ViewContainerRef;

  constructor(){
  }
}
