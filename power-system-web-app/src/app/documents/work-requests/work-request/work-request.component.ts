import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-work-request',
  templateUrl: './work-request.component.html',
  styleUrls: ['./work-request.component.css']
})
export class WorkRequestComponent implements OnInit {
  navLinks = [
    { path: 'basic-info', label: 'Basic information' },
    { path: 'state-changes', label: 'History of state changes' },
    { path: 'multimedia', label: 'Multimedia attachments' },
    { path: 'equipment', label: 'Equipment' },
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
