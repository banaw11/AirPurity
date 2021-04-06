import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbThemeModule, NbLayoutModule, NbCardModule ,NbActionsModule, NbSelectModule, NbAccordionModule, NbButtonModule, NbIconModule,
NbInputModule } from '@nebular/theme';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ChartsModule } from 'ng2-charts';
import { NgCircleProgressModule } from 'ng-circle-progress';
import { DatePipe } from '@angular/common';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { CityComponent } from './components/city/city.component';
import {HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { PmMeasuresChartComponent } from './modules/chart/pm-measures-chart/pm-measures-chart.component';
import { HomeComponent } from './components/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    PmMeasuresChartComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    HttpClientJsonpModule,
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: 'cosmic' }),
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
      titleColor:'#dbdbdb',
      unitsColor:'#dbdbdb',
      imageHeight:50,
      imageWidth:50

    }),
    ChartsModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
