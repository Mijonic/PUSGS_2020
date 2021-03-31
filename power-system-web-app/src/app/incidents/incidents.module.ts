import { RouterModule } from '@angular/router';
import { IncidentsComponent } from './incidents.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatRadioModule } from '@angular/material/radio';
import { IncidentComponent } from './incident/incident.component';
import {MatTabsModule} from '@angular/material/tabs';
import { MultimediaAttachmentsComponent } from './multimedia-attachments/multimedia-attachments.component';
import { BasicInformationComponent } from './basic-information/basic-information.component';
import { DevicesComponent } from './devices/devices.component';
import { ResolutionComponent } from './resolution/resolution.component';
import { CallsComponent } from './calls/calls.component';
import { CrewComponent } from './crew/crew.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { MatButtonModule } from '@angular/material/button';
import { BrowserModule } from '@angular/platform-browser';
import { MatFileUploadModule } from 'angular-material-fileupload';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';




@NgModule({
  declarations: [
    IncidentsComponent,
    IncidentComponent,
    MultimediaAttachmentsComponent,
    BasicInformationComponent,
    DevicesComponent,
    ResolutionComponent,
    CallsComponent,
    CrewComponent,
    EquipmentComponent
  ],
  exports:[
    IncidentsComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSortModule,
    MatExpansionModule,
    MatRadioModule,
    RouterModule,
    MatTabsModule,
    MatButtonModule,
    BrowserModule,
    MatFileUploadModule,  
    MatDatepickerModule,     
    MatNativeDateModule,
    MatSlideToggleModule
   
  
  ]
})
export class IncidentsModule { }
