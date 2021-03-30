import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbThemeModule, NbLayoutModule, NbCardModule ,NbActionsModule, NbSelectModule, NbAccordionModule, NbButtonModule, NbIconModule,
NbInputModule } from '@nebular/theme';
import { ChartsModule } from 'ng2-charts';
import { NgCircleProgressModule } from 'ng-circle-progress';
import { DatePipe } from '@angular/common';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { CityComponent } from './components/city/city.component';
import {HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { PmMeasuresChartComponent } from './modules/chart/pm-measures-chart/pm-measures-chart.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { MapComponent } from './modules/map/map.component';

@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    PmMeasuresChartComponent,
    MapComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    HttpClientJsonpModule,
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: 'default' }),
    NbLayoutModule,
    NbEvaIconsModule,
    NbCardModule,
    NbActionsModule,
    NbSelectModule,
    NbAccordionModule,
    NbButtonModule,
    NbIconModule,
    NbInputModule,
    NgCircleProgressModule.forRoot({
      radius: 25,
      outerStrokeWidth: 6,
      innerStrokeWidth: 2,
      animationDuration: 300,
      showSubtitle: false,
      titleFontSize: '10',
      titleFontWeight: '700',
      imageHeight:50,
      imageWidth:50

    }),
    ChartsModule,
    GoogleMapsModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
