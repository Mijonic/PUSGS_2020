import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ChooseIncidentDialogComponent } from 'app/documents/dialogs/choose-incident-dialog/choose-incident-dialog.component';

@Component({
  selector: 'app-work-request-basic-information',
  templateUrl: './work-request-basic-information.component.html',
  styleUrls: ['./work-request-basic-information.component.css']
})
export class WorkRequestBasicInformationComponent implements OnInit {
  documentTypes:string[] = ['Planned work', 'Unplanned work'];
  isEmergency:boolean = true;

  constructor(public dialog:MatDialog) { }

  ngOnInit(): void {
  }

  onChooseIncident()
  {
    const dialogRef = this.dialog.open(ChooseIncidentDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`The dialog was closed and choosen id is ${result}`);
    });
  }
 
}
