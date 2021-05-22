import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DeviceService } from 'app/services/device.service';
import { IncidentService } from 'app/services/incident.service';
import { LocationService } from 'app/services/location.service';
import { ValidationService } from 'app/services/validation.service';
import { Call } from 'app/shared/models/call.model';
import { Location } from 'app/shared/models/location.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-new-call',
  templateUrl: './new-call.component.html',
  styleUrls: ['./new-call.component.css']
})
export class NewCallComponent implements OnInit {
  @Input()
  reasons:string[];

  @Output() newCallFinished: EventEmitter<any> = new EventEmitter();

  incidentId: number;

  loadLocations = true;

  submitted = false;
  allLocations: Location[] = [];

  isNew:boolean = true;

  call: Call = new Call();
  newCall:Call = new Call();

  newCallForm = new FormGroup({
      callReason : new FormControl('', Validators.required),
      hazard : new FormControl('', [Validators.required, Validators.maxLength(100)]),
      comment : new FormControl('', [Validators.maxLength(100)]),
      callLocationControl : new FormControl('', Validators.required)
   });


   /*
constructor(private locationService:LocationService, private validationService:ValidationService, private deviceService:DeviceService,  private toastr: ToastrService,
    private router:Router, private route:ActivatedRoute) { }

  ngOnInit(): void {

    this.getAllLocations();
    
    const deviceId = this.route.snapshot.paramMap.get('id');
    if(deviceId != null && deviceId != "")
    {
      this.isNew = false;
      this.loadDevice(+deviceId);
    }
  
   

  }

   */

  constructor(private locationService:LocationService, private validationService:ValidationService, private deviceService:DeviceService,  private toastr: ToastrService,
    private router:Router, private route:ActivatedRoute, private incidentService: IncidentService) { }


    ngOnInit(): void {

      this.getAllLocations();
      
      const incidentId = this.route.snapshot.paramMap.get('id');
      if(incidentId != null && incidentId != "")
      {
        this.incidentId = +incidentId;
        
      }
    
     
  
    }

    
  getAllLocations()
  {
    this.locationService.getAllLocations().subscribe(
      data =>{
        this.allLocations = data;
        this.loadLocations = false;
   
      },
      error =>{
       

        if(error.error instanceof ProgressEvent)
        {
          this.getAllLocations();

        }else
        {
          this.toastr.error('Could not load locations.',"", {positionClass: 'toast-bottom-left'})
     
          this.router.navigate(['incidents']);
          this.loadLocations = false;
        }

      }
    )
  }
  

  onSave()
  {

    this.submitted = true;

    if(this.newCallForm.valid)
    {
     
      
         this.call.callReason = this.newCallForm.value.callReason;
         this.call.comment = this.newCallForm.value.comment;
         this.call.hazard = this.newCallForm.value.hazard;
         this.call.locationId = +this.newCallForm.value.callLocationControl;
         
       

          
          this.incidentService.addIncidentCall(this.incidentId, this.call).subscribe(
            data => {
            
              this.toastr.success("Incident call created successfully","", {positionClass: 'toast-bottom-left'});
              //this.router.navigate(['incidents']);

              this.newCallFinished.emit();
              },
              error=>{
                this.toastr.error(error.error, "", {positionClass: 'toast-bottom-left'});
              
              }
          );
      


    }else
    {
      this.validationService.validateAllFields(this.newCallForm);
    
    }

   
  }





  getAddressFromLocation(location: Location) {
        
    return  `${location.street} ${location.number}, ${location.city}, ${location.zip}`

  }

}
