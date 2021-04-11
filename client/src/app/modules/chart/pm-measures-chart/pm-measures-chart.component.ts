import { DatePipe } from '@angular/common';
import { Component, Input, OnChanges } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';
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
    this.lineChartData = [];
    this.lineChartLabels = [];
    this.pm10Data.length> 0 ? this.lineChartData.push(this.loadPM10()) : null;
    this.pm25Data.length> 0 ? this.lineChartData.push(this.loadPM25()) : null;
  
    this.lineChartLabels = this.convertDates();
  }


  convertDates(): string[]{
    let dates: string[] = [];
    if(this.pm10Data.length >0){
      this.pm10Data.map(x => x.dateFormat).forEach( x=> {
        dates.push(this.datePipe.transform(x, 'MM-dd HH:mm'));
      })
    }
    else if(this.pm25Data.length >0){
      this.pm25Data.map(x => x.dateFormat).forEach( x=> {
        dates.push(this.datePipe.transform(x, 'MM-dd HH:mm'));
      })
    }
    return dates.reverse();
  }

  loadPM10(): ChartDataSets{
    let dataSet: ChartDataSets = {};
    dataSet.data = this.pm10Data.map(x => x.value).reverse()
    dataSet.label = "PM 10"
    return dataSet
  }

  loadPM25(): ChartDataSets{
    let dataSet: ChartDataSets = {};
    dataSet.data = this.pm25Data.map(x => x.value).reverse()
    dataSet.label = "PM 25"
    return dataSet
  }

}
