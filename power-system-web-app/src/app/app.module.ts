import { AuthGuardService } from './auth/auth-guard.service';

import { WorkRequestService } from './services/work-request.service';
import { SettingsModule } from './settings/settings.module';
import { IncidentsModule } from './incidents/incidents.module';
import { CrewsModule } from './crews/crews.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardModule } from './dashboard/dashboard.module';
import { NavigationModule } from './navigation/navigation.module';
import {  NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule, WORK_REQUEST_SERVICE_TOKEN } from './app-routing.module';
import { AppComponent } from './app.component';
import { DatePipe } from '@angular/common';
import { FrontModule } from './front/front.module';
import { DocumentsModule } from './documents/documents.module';
import { UsersModule } from './users/users.module';
import { NotificationsModule } from './notifications/notifications.module';
import { DevicesModule } from './devices/devices.module';
import { ToastrModule } from 'ngx-toastr';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { JwtModule } from "@auth0/angular-jwt";

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NavigationModule,
    FrontModule,
    DashboardModule,
    DocumentsModule,
    BrowserAnimationsModule,
    UsersModule,
    CrewsModule,
    IncidentsModule,
    SettingsModule,
    NotificationsModule,
    DevicesModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains:['localhost:44372']
      }
    })
  ],

  providers: [
    DatePipe,
    {provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
   /* {
      provide: INCIDENT_SERVICE_TOKEN,
      useClass: Define service here
   },*/
   {
      provide: WORK_REQUEST_SERVICE_TOKEN,
      useClass: WorkRequestService
   }
   /*Define other services here*/
   ,
   AuthGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
