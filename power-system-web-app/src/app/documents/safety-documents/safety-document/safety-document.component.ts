import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-safety-document',
  templateUrl: './safety-document.component.html',
  styleUrls: ['./safety-document.component.css']
})
export class SafetyDocumentComponent implements OnInit {

  navLinks = [
    { path: 'basic-info', label: 'Basic information' },
    { path: 'state-changes', label: 'History of state changes' },
    { path: 'multimedia', label: 'Mutlimedia attachments' },
    { path: 'equipment', label: 'Equipment' },
    { path: 'checklist', label: 'Checklist' },
  ];
  constructor() { }

  ngOnInit(): void {
  }

}
