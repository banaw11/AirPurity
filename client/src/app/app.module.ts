import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbThemeModule, NbLayoutModule, NbCardModule ,NbActionsModule, NbSelectModule, NbAccordionModule, NbButtonModule, NbIconModule,
NbInputModule } from '@nebular/theme';
import { NgCircleProgressModule } from 'ng-circle-progress';
import { NgxEchartsModule } from 'ngx-echarts';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { CityComponent } from './components/city/city.component';
import {HttpClientModule } from '@angular/common/http';
import { ChartComponent } from './modules/chart/chart.component';

@NgModule({
  declarations: [
    AppComponent,
    CityComponent,
    ChartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
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
    NgxEchartsModule.forRoot({echarts: () => import('echarts'),})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
