import { SettingsModule } from './settings/settings.module';
import { IncidentsModule } from './incidents/incidents.module';
import { CrewsModule } from './crews/crews.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardModule } from './dashboard/dashboard.module';
import { NavigationModule } from './navigation/navigation.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DatePipe } from '@angular/common';
import { FrontModule } from './front/front.module';
import { DocumentsModule } from './documents/documents.module';
import { UsersModule } from './users/users.module';
import { NotificationsModule } from './notifications/notifications.module';




@NgModule({
  declarations: [
    AppComponent
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
    NotificationsModule
    
  ],

  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
