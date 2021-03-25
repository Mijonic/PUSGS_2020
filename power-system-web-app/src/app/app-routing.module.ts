import { WorkRequestsComponent } from './documents/work-requests/work-requests/work-requests.component';
import { WorkPlansComponent } from './documents/work-plans/work-plans.component';
import { MainNavbarComponent } from './navigation-bars/main-navbar/main-navbar.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FrontPageComponent } from './front-page/front-page.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: 'register', component: RegistrationComponent },
  { path: '', component: FrontPageComponent  },
  //{ path: 'login', component: LoginComponent },
  { path: 'work-plans', component: WorkPlansComponent },
  { path: 'work-requests', component: WorkRequestsComponent }
  //{ path: '', component: MainNavbarComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { 


}
