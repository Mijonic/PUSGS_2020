import { IncidentMapDisplay } from './../shared/models/incident-map-display.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Incident } from 'app/shared/models/incident.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IncidentService {

  constructor(private http: HttpClient) { }
  
  getIncidentLocation(incidentId:number):Observable<Location>{
    let requestUrl = environment.serverURL.concat(`incidents/${incidentId}/location`);
    return this.http.get<Location>(requestUrl);
  }

  getUnassignedIncidents():Observable<Incident[]>{
    let requestUrl = environment.serverURL.concat(`incidents/unassigned`);
    return this.http.get<Incident[]>(requestUrl);
  }

  getUnresolvedIncidents():Observable<IncidentMapDisplay[]>{
    let requestUrl = environment.serverURL.concat(`incidents/unresolved`);
    return this.http.get<IncidentMapDisplay[]>(requestUrl);
  }
}
