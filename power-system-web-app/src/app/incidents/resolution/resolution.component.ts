import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TabMessagingService } from 'app/services/tab-messaging.service';

@Component({
  selector: 'app-resolution',
  templateUrl: './resolution.component.html',
  styleUrls: ['./resolution.component.css']
})
export class ResolutionComponent implements OnInit {
  failureSubcauses:string[] = ['failuresub1', 'failuresub2', 'failuresub3'];
  weatherSubcauses:string[] = ['Storm', 'Rain', 'Wind', 'Snow'];
  humanErrorSubcauses:string[] = ['1', '2', '3', '4'];
  subcauses:string[] = [];
  resolutionFormControl= new FormControl();


  constructor(private tabMessaging:TabMessagingService, private route:ActivatedRoute) { }

  ngOnInit(): void {

    const incidentId = this.route.snapshot.paramMap.get('id');
    if(incidentId && incidentId != "")
    {
      this.tabMessaging.showEdit(+incidentId);
     // this.isNew = false;
      //this.workReqId = +wrId;
     /// this.loadWorkRequest(this.workReqId);
    }

    window.dispatchEvent(new Event('resize'));
    
    
  }

  causeSelectionChanged(event:any)
  {
    if(event.value === 'Failure')
    {
      this.subcauses = this.failureSubcauses;

    }else if(event.value === 'Weather')
    {
      this.subcauses = this.weatherSubcauses;

    }else if(event.value === 'HumanError')
    {
      this.subcauses = this.humanErrorSubcauses;
    }
  }

  get shouldShowSubcauses():boolean{
    return this.subcauses.length > 0;
  }

}
