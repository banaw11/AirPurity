import { Component, Input, NgModule, OnInit } from '@angular/core';
import { Measure } from 'src/app/models/measure';
import { EChartsOption } from 'echarts';


@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent implements OnInit {
@Input() pm10Data: Measure[];
@Input() pm25Data: Measure[];

  options: EChartsOption;

  ngOnInit(): void {
      const xAxisData = [];
      const data1 = [];
      const data2 =[]

      this.pm10Data.forEach(x => {
        xAxisData.push(x.dateFormat);
        data1.push(x.value);
      })

      this.pm25Data.forEach(x => {
        data2.push(x.value);
      })

      this.options = {
        legend: {
          data: ['PM10', 'PM2.5'],
          align: 'left',
        },
        tooltip: {},
        xAxis: {
          data: xAxisData,
          silent: false,
          splitLine: {
            show: false,
          },
        },
        yAxis: {},
        series: [
          {
            name: 'PM10',
            type: 'bar',
            data: data1,
            animationDelay: (idx) => idx * 10,
          },
          {
            name: 'PM2.5',
            type: 'bar',
            data: data2,
            animationDelay: (idx) => idx * 10 + 100,
          },
        ],
        animationEasing: 'elasticOut',
        animationDelayUpdate: (idx) => idx * 5,
      };
  }

}
