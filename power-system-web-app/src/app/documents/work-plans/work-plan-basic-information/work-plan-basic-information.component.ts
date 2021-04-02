import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-work-plan-basic-information',
  templateUrl: './work-plan-basic-information.component.html',
  styleUrls: ['./work-plan-basic-information.component.css']
})
export class WorkPlanBasicInformationComponent implements OnInit {
  documentTypes:string[] = ['Planned work', 'Unplanned work'];

  constructor() { }

  ngOnInit(): void {
  }

}
