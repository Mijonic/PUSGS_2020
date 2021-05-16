import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../services/user.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Login } from 'app/shared/models/login.model';
import { User } from 'app/shared/models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    username:new FormControl('', Validators.required),
    password:new FormControl('', Validators.required)
  });
  @ViewChild('closeBtn') closeBtn: ElementRef;
  @ViewChild('resetBtn') resetBtn: ElementRef;

  credentials:Login = new Login();

  constructor(private usersService:UserService, private toastr:ToastrService, private router:Router) { }

  ngOnInit(): void {
  }

  onSubmit()
  {
    if(this.loginForm.valid)
    {
      this.credentials.username = this.loginForm.controls['username'].value;
      this.credentials.password = this.loginForm.controls['password'].value;
      this.usersService.login(this.credentials).subscribe(
        data =>{
          const token = data.token;
          const user:User = data.userData;
          localStorage.setItem("jwt", token);
          localStorage.setItem("user", JSON.stringify(user));
          this.resetBtn.nativeElement.click();
          this.closeBtn.nativeElement.click();
          this.router.navigate(["/dashboard"]);
          this.toastr.success("Login successfull");
        },
        error =>{
          this.toastr.error(error.error);
        }
      )
    }else
    {
      this.toastr.info("Please fill all form fields.");
    }

  }

  isValid(controlName:string)
  { 
    return this.loginForm.controls[controlName].valid
  }

  isInvalid(controlName:string)
  {
    return this.loginForm.controls[controlName].invalid
  }


}
