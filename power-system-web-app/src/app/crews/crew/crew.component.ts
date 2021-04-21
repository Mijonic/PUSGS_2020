import { UserService } from './../../services/user.service';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { User } from 'app/shared/models/user.model';
import { Crew } from 'app/shared/models/crew.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-crew',
  templateUrl: './crew.component.html',
  styleUrls: ['./crew.component.css']
})
export class CrewComponent implements OnInit {
  unassignedCrewMembers :User[] = [];
  crew:Crew = new Crew();
  crewForm = new FormGroup({
  crewNameControl : new FormControl('', Validators.required)
});

  constructor(private userService:UserService){}
  ngOnInit(): void {
    this.userService.getAllUnassignedCrewMembers().subscribe( x=> {
        this.unassignedCrewMembers = x;
    });
  }

  saveChanges()
  {
//this.crewForm.hasError
  }

  drop(event: CdkDragDrop<User[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    }
  }

  addMember(member:User)
  {
    transferArrayItem(this.unassignedCrewMembers,
      this.crew.crewMembers,
      this.unassignedCrewMembers.indexOf(member),
      0);

  }

  removeMember(member:User)
  {
    transferArrayItem(this.crew.crewMembers,
      this.unassignedCrewMembers,
      this.crew.crewMembers.indexOf(member),
      0);
  }

}
