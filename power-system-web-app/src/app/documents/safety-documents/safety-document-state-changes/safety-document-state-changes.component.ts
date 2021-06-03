import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { UserService } from 'app/services/user.service';
import { ValidationService } from 'app/services/validation.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-safety-document-state-changes',
  templateUrl: './safety-document-state-changes.component.html',
  styleUrls: ['./safety-document-state-changes.component.css']
})
export class SafetyDocumentStateChangesComponent implements OnInit {

  constructor(public dialog:MatDialog, private validation:ValidationService, private route:ActivatedRoute, private userService:UserService,
    private toastr:ToastrService, private router:Router,
   private tabMessaging:TabMessagingService) {
    // Create 100 users
  
  }

  ngOnInit(): void {
    const safetyDocumentId = this.route.snapshot.paramMap.get('id');
    //this.loadDevices(+wrId!);
    this.tabMessaging.showEdit(+safetyDocumentId!);
  }


}
