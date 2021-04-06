import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-safety-document-basic-information',
  templateUrl: './safety-document-basic-information.component.html',
  styleUrls: ['./safety-document-basic-information.component.css']
})
export class SafetyDocumentBasicInformationComponent implements OnInit {

  documentTypes:string[] = ['Planned work', 'Unplanned work'];
  date = new Date((new Date().getTime()));

  constructor() { }

  ngOnInit(): void {
  }

}
