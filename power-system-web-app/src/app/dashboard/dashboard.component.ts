import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor() {
    window.dispatchEvent(new Event('resize'));
   }

  ngOnInit(): void {
    //window.dispatchEvent(new Event('resize'));
  }

}
