import { DatePipe } from '@angular/common';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Measure } from 'src/app/models/measure';

@Component({
  selector: 'app-pm-measures-chart',
  templateUrl: './pm-measures-chart.component.html',
  styleUrls: ['./pm-measures-chart.component.scss']
})
export class PmMeasuresChartComponent implements OnChanges{
  @Input() pm10Data: Measure[];
  @Input() pm25Data: Measure[];
  
  public lineChartData: ChartDataSets[] = [];
  public lineChartLabels: Label[] = [];
  public lineChartOptions: ChartOptions = {
    responsive: true,
  };
  public lineChartColors: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(255,0,0,0.3)',
    },
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [];

  constructor(private datePipe: DatePipe) { }

  ngOnChanges(): void {
    this.lineChartData = [
      { data: this.pm10Data.map(x => x.value).reverse(),
         label: 'PM 10' },
      { data: this.pm25Data.map(x => x.value).reverse(), label: 'PM 2.5' },
    ];

    this.lineChartLabels = this.convertDates();
  }


  convertDates(): string[]{
    let dates: string[] = [];
    this.pm10Data.map(x => x.dateFormat).forEach( x=> {
      dates.push(this.datePipe.transform(x, 'MM-dd HH:mm'));
    })
    return dates.reverse();
  }

}
