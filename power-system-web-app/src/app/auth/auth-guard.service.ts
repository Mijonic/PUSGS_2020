import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private jwtHelper: JwtHelperService, private router: Router, private toastr:ToastrService) {
  }
  canActivate(route:ActivatedRouteSnapshot) {
    const token = localStorage.getItem("jwt");

    if(!token)
    {
      this.toastr.warning("Please log in.");
      this.router.navigate(["/"]);
      return false;
    }

    if (this.jwtHelper.isTokenExpired(token)){


      this.toastr.warning("Your session has expired, plase log in again.");
      this.router.navigate(["/"]);
      return false;
    }

    if (route.data && route.data.roles) {
      let roles:string[] = route.data.roles;
      console.log(this.jwtHelper.decodeToken(token));
      let tokenData = this.jwtHelper.decodeToken(token);
      let userRole = tokenData['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      if(roles.indexOf(userRole) == -1)
      {
        this.toastr.warning("Your are not authorized to view this.");
        this.router.navigate(["/dashboard"]);
        return false;
      }
    }

    return true;

  }
}
