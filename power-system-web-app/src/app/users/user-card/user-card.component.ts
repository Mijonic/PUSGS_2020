import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../services/user.service';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from 'app/shared/models/user.model';
import { Location } from "app/shared/models/location.model";

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {
  @Input()
  user!:User;
  @Output() reload: EventEmitter<any> = new EventEmitter();

  constructor(public datePipe:DatePipe, private userService:UserService, private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  getDateForDisplay(date:Date)
  {
    return this.datePipe.transform(date, 'dd-MM-yyyy');

  }

  getUserTypeDisplay(type:string){
    if(type === 'CREW_MEMBER')
      return 'Crew member';

    if(type === 'DISPTACHER')
      return 'Disptacher';

    if(type === 'WORKER')
      return 'Wroker';

    if(type === 'ADMIN')
      return 'Admin';

    if(type === 'CONSUMER')
      return 'Consumer';
    
    return "Unknown";
  }

  getLocationDisplayString(location:Location)
  {
    return `${location.street}, ${location.city}`;
  }

  approveUser(id:number){
    this.userService.approveUser(id).subscribe(
      data =>
        {
          this.toastr.success("User approved.");
          this.reload.emit();
        },
      error =>{
        this.toastr.error(error.error);
        this.reload.emit();
      }
      );
  }

  denyUser(id:number){
    this.userService.denyUser(id).subscribe(
      data =>
        {
          this.toastr.success("User denied.");
          this.reload.emit();
        },
      error =>{
        this.toastr.error(error.error);
        this.reload.emit();
      }
      );
  }

}
