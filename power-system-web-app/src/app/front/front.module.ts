import { NavigationModule } from './../navigation/navigation.module'
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FrontPageComponent } from './front-page/front-page.component';
import { ParticlesComponent } from './particles/particles.component';
import { NgParticlesModule } from 'ng-particles';
import { MatIconModule } from '@angular/material/icon';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';



@NgModule({
  declarations: [
    FrontPageComponent,
    ParticlesComponent,
    RegistrationComponent,
    LoginComponent,

  ],
  exports:[
    FrontPageComponent,
    ParticlesComponent
  ],

  imports: [
    CommonModule,
    NgParticlesModule,
    MatIconModule,
    NavigationModule
  ]
})
export class FrontModule { }
