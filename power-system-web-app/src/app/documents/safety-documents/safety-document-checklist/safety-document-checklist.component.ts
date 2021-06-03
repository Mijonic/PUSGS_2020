import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { DisplayService } from 'app/services/display.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-safety-document-checklist',
  templateUrl: './safety-document-checklist.component.html',
  styleUrls: ['./safety-document-checklist.component.css']
})
export class SafetyDocumentChecklistComponent implements OnInit {

  constructor(public dialog:MatDialog,  private route:ActivatedRoute, private toastr:ToastrService,
    private tabMessaging:TabMessagingService, public display:DisplayService) {
  }

  ngOnInit(): void {
    const safetyDocumentId = this.route.snapshot.paramMap.get('id');
    //this.loadDevices(+wrId!);
    this.tabMessaging.showEdit(+safetyDocumentId!);
  }

}
