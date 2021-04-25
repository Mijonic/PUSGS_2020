
import { NavbarMessagingService } from 'app/services/navbar-messaging.service';
import { UserService } from './../../services/user.service';
import { CrewService } from './../../services/crew.service';
import { LocationService } from './../../services/location.service';
import { User } from './../../shared/models/user.model';
import { ToastrService } from 'ngx-toastr';
import { ValidationService } from './../../services/validation.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Location } from 'app/shared/models/location.model';
import { Crew } from 'app/shared/models/crew.model';

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
    dateOfBirth: new FormControl('', [this.birthDayValidator, Validators.required]),
    password: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    confirmPassword: new FormControl('', Validators.required),
    location: new FormControl('', Validators.required),
    crew: new FormControl('', Validators.required)

  }, { validators: this.matchingPasswords });

  user:User = new User();
  locations:Location[] = [];
  crews:Crew[] = [];
  showCrews:boolean = false;
  @ViewChild('closeBtn') closeBtn: ElementRef;

  constructor(private validation:ValidationService, private toastr:ToastrService, private locationService:LocationService, 
    private crewService:CrewService, private userService:UserService, private navbarService:NavbarMessagingService) {
   }

  ngOnInit(): void {
    this.loadLocations();
    this.loadCrews();
  }

  loadLocations()
  {
    this.locationService.getAllLocations().subscribe(
      data =>{
        this.locations = data;
      }
    )
  }

  loadCrews()
  {
    this.crewService.getAllCrews().subscribe(
      data =>{
        this.crews = data;
      }
    )
  }

  onSubmit()
  {
    if(this.registrationForm.valid)
    {

      this.user.location = new Location()
      this.user.location.id = +this.registrationForm.controls['location'].value;
      this.user.userType = this.registrationForm.controls['role'].value;
      if(this.user.userType === 'CREW_MEMBER')
        this.user.crewID = +this.registrationForm.controls['crew'].value;
      this.userService.createUser(this.user).subscribe(
        data =>{
            this.toastr.success("Registration successfull");
            this.closeBtn.nativeElement.click();
            this.navbarService.activateLogin();
            this.toastr.info("You can log in now.");
        },
        error =>{
            this.toastr.error(error.error);
        }
      )

    }
    else{
        this.validation.validateAllFields(this.registrationForm);
        this.toastr.info("Please check form fields again, there are some errors.");
    }

  }

  isValid(controlName:string)
  { 
    if(controlName === 'confirmPassword')
      return this.registrationForm.controls[controlName].valid && !this.registrationForm.hasError('mismatchedPasswords');
    return this.registrationForm.controls[controlName].valid
  }

  isInvalid(controlName:string)
  {
    if(controlName === 'confirmPassword')
      return this.registrationForm.controls[controlName].invalid || this.registrationForm.hasError('mismatchedPasswords');
    return this.registrationForm.controls[controlName].invalid
  }

  matchingPasswords(c: AbstractControl): {[key: string]: any} |null {
    let password = c.get(['password']);
    let confirmPassword = c.get(['confirmPassword']);

    if (password!.value !== confirmPassword!.value) {
      return { mismatchedPasswords: true };
    }
    return null;
  }

  birthDayValidator(c: AbstractControl): {[key: string]: any} |null {
    let birthDay = c.value;
    if(birthDay == '')
      return null;

    birthDay = new Date(birthDay);
    if ( birthDay.getFullYear() > new Date().getFullYear() - 18) {
      return { underage: true };
    }
    return null;
  }

  onRoleChange(value:string)
  {
    if(value === 'CREW_MEMBER')
      {
        this.showCrews = true;
        this.registrationForm.controls['crew'].setValidators(Validators.required);
        this.registrationForm.controls['crew'].updateValueAndValidity();
      }else
      {
        this.showCrews = false;
        this.registrationForm.controls['crew'].clearValidators(); 
        this.registrationForm.controls['crew'].updateValueAndValidity();
      }
  }


}
