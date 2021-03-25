import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegisterLoginNavbarComponent } from './navigation-bars/register-login-navbar/register-login-navbar.component';
import { MainNavbarComponent } from './navigation-bars/main-navbar/main-navbar.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { ParticlesComponent } from './particles/particles.component';
import {MatStepperModule} from '@angular/material/stepper';
import {NgParticlesModule} from "ng-particles";
import {MatIconModule} from '@angular/material/icon';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { WorkPlansComponent } from './documents/work-plans/work-plans.component';
import { NavigationSteeperComponent } from './navigation-bars/navigation-steeper/navigation-steeper.component';




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
    NavigationSteeperComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgParticlesModule,
    MatIconModule,
    MatStepperModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
