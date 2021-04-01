import { GenericDisplayModule } from './../generic-display/generic-display.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { UserCardComponent } from './user-card/user-card.component';
import { UsersComponent } from './users.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [
    UsersComponent,
    UserCardComponent
  ],
  exports:[
    UsersComponent,
    UserCardComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    GenericDisplayModule
  ]
})
export class UsersModule { }
