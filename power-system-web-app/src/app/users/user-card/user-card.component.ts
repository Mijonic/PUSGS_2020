import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { User } from 'app/shared/models/user.model';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {
  @Input()
  user!:User;

  constructor(public datePipe:DatePipe) { }

  ngOnInit(): void {
  }

  getDateForDisplay(date:Date)
  {
    return this.datePipe.transform(date, 'dd-MM-yyyy');

  }

}
