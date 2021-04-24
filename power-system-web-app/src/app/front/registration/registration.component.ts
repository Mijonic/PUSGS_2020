import { LocationService } from './../../services/location.service';
import { User } from './../../shared/models/user.model';
import { ToastrService } from 'ngx-toastr';
import { ValidationService } from './../../services/validation.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Location } from 'app/shared/models/location.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registrationForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(30)]),
    role: new FormControl('', Validators.required),
    firstName: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    lastName: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    dateOfBirth: new FormControl('', Validators.required),
    password: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    confirmPassword: new FormControl('', Validators.required),
    location: new FormControl('', Validators.required),

  });

  user:User = new User();
  locations:Location[] = [];

  constructor(private validation:ValidationService, private toastr:ToastrService, private locationService:LocationService) {
   }

  ngOnInit(): void {
    this.loadLocations();
  }

  loadLocations()
  {
    this.locationService.getAllLocations().subscribe(
      data =>{
        this.locations = data;
      }
    )
  }

  onSubmit()
  {
    if(this.registrationForm.valid)
    {

    }
    else{
        this.validation.validateAllFields(this.registrationForm);
        this.toastr.info("Please check form fields again, there are some errors.");
    }

  }

  isValid(controlName:string)
  {
    return this.registrationForm.controls[controlName].valid
  }

  isInvalid(controlName:string)
  {
    return this.registrationForm.controls[controlName].invalid
  }


  /*checkPasswords(group: FormGroup):ValidatorFn { 
    const password = group.get('password')!.value;
    const confirmPassword = group.get('confirmPassword')!.value;
  
    //return password === confirmPassword ? null : { notSame: true }     
  }*/

}
