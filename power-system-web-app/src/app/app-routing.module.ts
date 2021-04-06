import { CrewsComponent } from './crews/crews.component';
import { WorkMapComponent } from './map/work-map/work-map.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CrewComponent } from './crews/crew/crew.component';
import { RegistrationComponent } from './front/registration/registration.component';
import { FrontPageComponent } from './front/front-page/front-page.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { WorkPlansComponent } from './documents/work-plans/work-plans.component';
import { WorkRequestsComponent } from './documents/work-requests/work-requests/work-requests.component';
import { UsersComponent } from './users/users.component';
import { IncidentsComponent } from './incidents/incidents.component';
import { EditProfileComponent } from './settings/edit-profile/edit-profile.component';
import { IncidentComponent } from './incidents/incident/incident.component';
import { WorkPlanComponent } from './documents/work-plans/work-plan/work-plan.component';
import { SafetyDocumentsComponent } from './documents/safety-documents/safety-documents/safety-documents.component';
import { SafetyDocumentComponent } from './documents/safety-documents/safety-document/safety-document.component';



const routes: Routes = [
  { path: 'register', component: RegistrationComponent,  outlet:"primary"  },
  { path: '', component: FrontPageComponent,  outlet:"front" },
  { path: 'work-plans', component: WorkPlansComponent,  outlet:"primary"  },
  { path: 'work-requests', component: WorkRequestsComponent,  outlet:"primary"  },
  { path: 'dashboard', component: DashboardComponent,  outlet:"primary" },
  { path: 'map', component: WorkMapComponent,  outlet:"primary" },
  { path: 'crews', component: CrewsComponent, outlet: "primary" },
  { path: 'crew', component: CrewComponent, outlet: "primary" } ,
  { path: 'edit-profile', component: EditProfileComponent, outlet: "primary"},
  { path: 'users', component: UsersComponent, outlet: "primary"},
  { path: 'incidents', component: IncidentsComponent, outlet: "primary"},
  { path: 'incident', component: IncidentComponent, outlet: "primary"},
  { path: 'work-plan', component: WorkPlanComponent, outlet: "primary"},
  { path: 'safety-documents', component: SafetyDocumentsComponent, outlet: "primary"},
  { path: 'safety-document', component: SafetyDocumentComponent, outlet: "primary"}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {


}
