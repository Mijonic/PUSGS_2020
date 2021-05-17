import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { ActivatedRoute, Router } from '@angular/router';
import { IncidentService } from 'app/services/incident.service';
import { TabMessagingService } from 'app/services/tab-messaging.service';
import { ValidationService } from 'app/services/validation.service';
import { Incident } from 'app/shared/models/incident.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-basic-information',
  templateUrl: './basic-information.component.html',
  styleUrls: ['./basic-information.component.css']
})
export class BasicInformationComponent implements OnInit {

  documentTypes = [
    {display:'Planned work', value:'PLANNED'},
    {display:'Unplanned work', value:'UNPLANNED'},
   ];

   incidentStatuses = [
    {display:'Unresolved', value:'UNRESOLVED'},
    {display:'Resolved', value:'RESOLVED'},
   ];

   

   // dodati validatore za vreme
   incidentForm = new FormGroup({
     
    confirmed: new FormControl('', Validators.required),
    eta: new FormControl('', Validators.required),
    ata: new FormControl(''),
    etr: new FormControl(''),
    workBeginDate: new FormControl(''),
    incidentDateTime: new FormControl(''),
    voltageLevel: new FormControl('', [Validators.required, Validators.min(0.1)]),
    description: new FormControl('', [Validators.maxLength(100)]),
    workType: new FormControl('', Validators.required),
    incidentStatus: new FormControl('', Validators.required)
   

  });

  
   

  isNew = true;
  isLoading:boolean = false;
  incidentId:number;

  incident: Incident = new Incident();
    
  



  constructor(private tabMessaging:TabMessagingService, private route:ActivatedRoute,
    private validation:ValidationService,   private toastr:ToastrService, private incidentService:IncidentService, private router:Router) { }

  ngOnInit(): void {
    const incidentId = this.route.snapshot.paramMap.get('id');
    if(incidentId && incidentId != "")
    {
      this.tabMessaging.showEdit(+incidentId);
      this.isNew = false;
      this.incidentId = +incidentId;
      this.loadIncident(this.incidentId);
    }
  }


  loadIncident(id:number)
  {
    this.isLoading = true;
      this.incidentService.getIncidentById(id).subscribe(
        data =>{
          this.isLoading = false;
          this.incident = data;
          this.populateControls(this.incident);
        } ,
        error =>{
          if(error.error instanceof ProgressEvent)
          {
            this.loadIncident(id);
          }else
          {
            this.toastr.error('Could not load incident.',"", {positionClass: 'toast-bottom-left'})
       
            this.router.navigate(['incidents']);
            this.isLoading = false;
          }
        }
      );
  }

  populateControls(incident:Incident)
  {
      this.incidentForm.controls['confirmed'].setValue(incident.confirmed);
      this.incidentForm.controls['eta'].setValue(incident.eta);
      this.incidentForm.controls['ata'].setValue(incident.ata);
      this.incidentForm.controls['etr'].setValue(incident.etr);
      this.incidentForm.controls['workBeginDate'].setValue(incident.workBeginDate);
      this.incidentForm.controls['incidentDateTime'].setValue(incident.incidentDateTime);
      this.incidentForm.controls['voltageLevel'].setValue(incident.voltageLevel);
      this.incidentForm.controls['description'].setValue(incident.description);
      this.incidentForm.controls['workType'].setValue(incident.workType);
      this.incidentForm.controls['incidentStatus'].setValue(incident.incidentStatus);
    
      //this.loadUserData(workRequest.userID);
  }

  populateModelFromFields()
  {
    
     
      this.incident.confirmed =  this.incidentForm.controls['confirmed'].value;
      this.incident.eta = new Date(this.incidentForm.controls['eta'].value);
      this.incident.ata = new Date(this.incidentForm.controls['ata'].value);
      this.incident.etr = this.incidentForm.controls['etr'].value;
      this.incident.workBeginDate = this.incidentForm.controls['workBeginDate'].value;
      this.incident.incidentDateTime = this.incidentForm.controls['incidentDateTime'].value;
      this.incident.voltageLevel = +this.incidentForm.controls['voltageLevel'].value;
      this.incident.description = this.incidentForm.controls['description'].value;
      this.incident.workType = this.incidentForm.controls['workType'].value;
      this.incident.incidentStatus = this.incidentForm.controls['incidentStatus'].value;
    
      this.incident.userId = 4;
  }



  onSave()
  {
    if(this.incidentForm.valid)
    {
        this.populateModelFromFields();
        this.isLoading = true;
        if(this.isNew)
        {
          this.incidentService.createNewIncident(this.incident).subscribe(
            data =>{
              console.log(data)
              this.toastr.success('Incident created successfully',"", {positionClass: 'toast-bottom-left'})
              this.router.navigate(['incident/basic-info', data.id])
            },
            error =>{
            this.isLoading = false;
              if(error.error instanceof ProgressEvent)
                {
                  
                  this.toastr.error('Server is unreachable',"", {positionClass: 'toast-bottom-left'})
                }else
                {
                  
                  this.toastr.error(error.error,"", {positionClass: 'toast-bottom-left'})
                }
              
            }
          )
        }else
        {
          this.incidentService.updateIncident(this.incident).subscribe(
            data =>{
              this.toastr.success('Incident updated successfully',"", {positionClass: 'toast-bottom-left'})
              this.incident = data;
              this.isLoading = false;
            },
            error =>{
            this.isLoading = false;
              if(error.error instanceof ProgressEvent)
                {
                  
                  this.toastr.success('Server is unreachable',"", {positionClass: 'toast-bottom-left'})
                }else
                {
                  
                  this.toastr.error(error.error,"", {positionClass: 'toast-bottom-left'})
                }
                
              
            }
          )
        }

    }else
    {
      this.validation.validateAllFields(this.incidentForm);
    }

  }



 



}
