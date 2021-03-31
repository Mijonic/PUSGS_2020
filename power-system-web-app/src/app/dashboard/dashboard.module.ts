import { MatIconModule } from '@angular/material/icon';
import { DashboardComponent } from './dashboard.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgApexchartsModule } from 'ng-apexcharts';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  exports:[
    DashboardComponent
  ],

  imports: [
    CommonModule,
    NgApexchartsModule,  
    MatIconModule
  ]
})
export class DashboardModule { }
