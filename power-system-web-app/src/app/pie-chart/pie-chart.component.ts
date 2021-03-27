import { Component, OnInit, ViewChild } from '@angular/core';
import { Colorize } from '@material-ui/icons';
import { ApexChart, ApexDataLabels, ApexFill, ApexLegend, ApexNonAxisChartSeries, ApexResponsive, ChartComponent } from "ng-apexcharts";


@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {

  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions: any = {
    series: [55, 35, 10],
    labels:  ['WP', 'WR', 'RD'],
    title: {
      text: 'DOCUMENTS',
      align: 'center',
      
      offsetX: 0,
      offsetY: 0,
      floating: false,
      style: {
        fontSize:  '16px',
        fontWeight:  'bold',
        fontFamily:  'Arial',
        color:  'white'
      },
    },  
   
    chart: {
      width: '100%',
      type: "donut",
      foreColor: 'white'
      
    },
    dataLabels: {
      enabled: true,
    },
    fill: {
      type: "gradient"
    },

    
    
    legend: {
      formatter: function(val: any, opts: any) {
        return val + " - " + opts.w.globals.series[opts.seriesIndex];
      }
      
    
    },
    responsive: [
      {
        breakpoint: 480,
        options: {
          chart: {
            width: 200
          },
          legend: {
            position: "bottom"
          }
        }
      }
    ]
  };

  

  constructor() {
   
  }

  ngOnInit(): void {
    
  }

  

}

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
  fill: ApexFill;
  legend: ApexLegend;
  dataLabels: ApexDataLabels;
};
