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
import { NgxSpinnerModule } from "ngx-spinner";
import { DatePipe } from '@angular/common';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { CityComponent } from './components/city/city.component';
import {HttpClientModule, HttpClientJsonpModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { PmMeasuresChartComponent } from './modules/chart/pm-measures-chart/pm-measures-chart.component';
import { HomeComponent } from './components/home/home.component';
import { StationComponent } from './components/station/station.component';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { CustomDropdownComponent } from './modules/forms/custom-dropdown/custom-dropdown.component';
import { StopNotificationComponent } from './components/stop-notification/stop-notification.component';
import { NotificationComponent } from './components/notification/notification.component';
import { CustomInputComponent } from './modules/forms/custom-input/custom-input.component';
import { EmailConfirmationComponent } from './components/email-confirmation/email-confirmation.component';

@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    PmMeasuresChartComponent,
    HomeComponent,
    StationComponent,
    CustomDropdownComponent,
    StopNotificationComponent,
    NotificationComponent,
    CustomInputComponent,
    EmailConfirmationComponent
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
    NgxSpinnerModule,
    ChartsModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    DatePipe,
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
