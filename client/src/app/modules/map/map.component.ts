import { HttpClient } from '@angular/common/http';
import { Component, OnChanges, SimpleChanges } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnChanges {
  apiLoaded: Observable<boolean>;
  center: google.maps.LatLngLiteral = {lat: 52.4144, lng: 16.9211};
  zoom: 4;

  constructor(private http: HttpClient) {
    this.apiLoaded = this.http.jsonp('https://maps.googleapis.com/maps/api/js?key='+environment.googleApiKey, 'callback')
        .pipe(
          map(() => true),
          catchError(() => of(false)),
        );
   
    
   }
  ngOnChanges(changes: SimpleChanges): void {
    
  }

  moveMap(event: google.maps.MapMouseEvent) {
    this.center = (event.latLng.toJSON());
  }



}
