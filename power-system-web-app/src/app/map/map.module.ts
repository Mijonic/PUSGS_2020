import { MainNavbarComponent } from './../navigation-bars/main-navbar/main-navbar.component';
import { AppModule } from './../app.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkMapComponent } from './work-map/work-map.component';




@NgModule({
  declarations: [WorkMapComponent],
  imports: [
    CommonModule,
    AppModule
  ]
})
export class MapModule { }
