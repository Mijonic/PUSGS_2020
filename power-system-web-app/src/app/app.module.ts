import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { ParticlesComponent } from './particles/particles.component';

import {NgParticlesModule} from "ng-particles";


@NgModule({
  declarations: [
    AppComponent,
    FrontPageComponent,
    ParticlesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgParticlesModule   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
