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

  getAllIncidents():Observable<Incident[]>{
    let requestUrl = environment.serverURL.concat("incidents");
    return this.http.get<Incident[]>(requestUrl);
  }

  getIncidentById(id:number):Observable<Incident>{
    let requestUrl = environment.serverURL.concat(`incidents/${id}`);
    return this.http.get<Incident>(requestUrl);
  }

  createNewIncident(incident:Incident):Observable<Incident>{
    console.log(incident)
    let requestUrl = environment.serverURL.concat("incidents");
    return this.http.post<Incident>(requestUrl, incident);
  }

  updateIncident(incident: Incident):Observable<Incident>{
    let requestUrl = environment.serverURL.concat(`incidents/${incident.id}`);
    return this.http.put<Incident>(requestUrl, incident);
  }

  deleteIncident(id:number):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`incidents/${id}`);
    return this.http.delete(requestUrl);
  }


  //done
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
