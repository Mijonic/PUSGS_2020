import { ToastrService } from 'ngx-toastr';
import { StateChange } from './../../../../shared/models/state-change.model';
import { WorkRequestService } from './../../../../services/work-request.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-work-request-state-changes',
  templateUrl: './work-request-state-changes.component.html',
  styleUrls: ['./work-request-state-changes.component.css']
})
export class WorkRequestStateChangesComponent implements OnInit {

  stateChanges:StateChange[];
  isLoading:boolean;
  constructor(private workReqService:WorkRequestService, private toastr:ToastrService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    const wrId = this.route.snapshot.paramMap.get('id');
    if(wrId != null && wrId != '')
      this.loadStateChanges(+wrId);

  }

  loadStateChanges(id:number){
    this.isLoading = true;
    this.workReqService.getStateChanges(id).subscribe(
      data =>{
        this.stateChanges = data;
        this.isLoading = false;
      },
      error =>{
        if(error.error instanceof ProgressEvent)
        {
          this.loadStateChanges(id);
        }else{
          this.toastr.error(error.error);
          this.isLoading = false;
        }

      }
    )
  }

}
