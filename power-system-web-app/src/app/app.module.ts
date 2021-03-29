import { MapModule } from './map/map.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { SteeperWizardModule } from './steeper-wizard/steeper-wizard.module';
import { RegisterLoginNavbarComponent } from './navigation-bars/register-login-navbar/register-login-navbar.component';
import { MainNavbarComponent } from './navigation-bars/main-navbar/main-navbar.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { ParticlesComponent } from './particles/particles.component';
import {NgParticlesModule} from "ng-particles";
import {MatIconModule} from '@angular/material/icon';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { WorkPlansComponent } from './documents/work-plans/work-plans.component';
import { StepOneComponent } from './step-one/step-one.component';
import { WorkRequestsComponent } from './documents/work-requests/work-requests/work-requests.component';
import {MatSelectModule} from '@angular/material/select';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatTableModule} from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { DashboardComponent } from './dashboard/dashboard.component';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatRadioModule} from '@angular/material/radio';

import { NgApexchartsModule } from 'ng-apexcharts';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { CrewsComponent } from './crews/crews.component';
import { CrewComponent } from './crews/crew/crew.component';


@NgModule({
  declarations: [
    AppComponent,
    FrontPageComponent,
    ParticlesComponent,
    RegistrationComponent,
    LoginComponent,
    WorkPlansComponent,
    MainNavbarComponent,
    RegisterLoginNavbarComponent,
    StepOneComponent,
    WorkRequestsComponent,
    DashboardComponent,
    CrewsComponent,
    CrewComponent,
   

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgParticlesModule,
    MatIconModule,
    SteeperWizardModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatSlideToggleModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSortModule,
    MatExpansionModule,
    MatRadioModule,
    NgApexchartsModule,
    MatProgressSpinnerModule

  ],

  exports: [
    MainNavbarComponent,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
