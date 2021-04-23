import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'app/shared/models/user.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  roles: any[] = 
  [ {role:'ALL', value:'All'},
    {role:'WORKER', value:'Worker'},
    {role:'ADMIN', value:'Administrator'},
    {role:'DISPATCHER', value:'Dispatcher'},
    {role:'CREW_MEMBER', value:'Crew member'},
    ];
  users:User[] = [];
  allUsers:User[] = [];
  isLoading:boolean = true;

  constructor(private userService:UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.getUsers();
  }

  getUsers()
  {
    this.userService.getAllUsers().subscribe(
      data =>{
        this.allUsers = data;
        this.users = data;
        this.isLoading = false;
      }
    )
  }

  applyRoleFilter(role:any)
  {
    if(role.value == 'ALL')
    {
      this.users = this.allUsers;
      return;
    }
    this.users = this.allUsers.filter(x => x.userType == role.value);
  }

 

}
