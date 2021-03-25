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
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgParticlesModule,
    MatIconModule,
    SteeperWizardModule
    /*MatStepperModule,
    BrowserAnimationsModule*/
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
