import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-work-request',
  templateUrl: './work-request.component.html',
  styleUrls: ['./work-request.component.css']
})
export class WorkRequestComponent implements OnInit {
  isNew:boolean = true;
  navLinks = [
    { path: 'basic-info', label: 'Basic information', isDisabled: false },
    { path: 'state-changes', label: 'History of state changes', isDisabled: this.isNew },
    { path: 'multimedia', label: 'Multimedia attachments', isDisabled: this.isNew },
    { path: 'equipment', label: 'Equipment', isDisabled: this.isNew },
  ];

  

  constructor(private route:ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if(id && id!= "")
    {
      this.navLinks.forEach( f => {
          f.path.concat(`/${id}`);
          f.isDisabled = false;
      });
    }
  }

}
