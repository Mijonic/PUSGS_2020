import { CrewsComponent } from './crews/crews.component';
import { WorkMapComponent } from './map/work-map/work-map.component';
import { WorkRequestsComponent } from './documents/work-requests/work-requests/work-requests.component';
import { WorkPlansComponent } from './documents/work-plans/work-plans.component';
import { MainNavbarComponent } from './navigation-bars/main-navbar/main-navbar.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FrontPageComponent } from './front-page/front-page.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';



const routes: Routes = [
  { path: 'register', component: RegistrationComponent,  outlet:"primary"  },
  { path: '', component: FrontPageComponent,  outlet:"front" },
  //{ path: 'login', component: LoginComponent },
  { path: 'work-plans', component: WorkPlansComponent,  outlet:"primary"  },
  { path: 'work-requests', component: WorkRequestsComponent,  outlet:"primary"  },
  //{ path: '', component: MainNavbarComponent }
  { path: 'dashboard', component: DashboardComponent,  outlet:"primary" },
  { path: 'map', component: WorkMapComponent,  outlet:"primary" },
  { path: 'crews', component: CrewsComponent, outlet: "primary" },
  { path: 'edit-profile', component: EditProfileComponent, outlet: "primary"}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {


}
