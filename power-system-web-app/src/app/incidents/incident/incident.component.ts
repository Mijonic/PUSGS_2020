import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-incident',
  templateUrl: './incident.component.html',
  styleUrls: ['./incident.component.css']
})
export class IncidentComponent implements OnInit {
  navLinks = [
    { path: 'basic-info', label: 'Basic information' },
    { path: 'calls', label: 'Calls' },
    { path: 'crew', label: 'Crew' },
    { path: 'multimedia', label: 'Multimedia attachments' },
    { path: 'devices', label: 'Devices' },
    { path: 'resolution', label: 'Resolution' },

  ];

  constructor() { }

  ngOnInit(): void {
  }

}
