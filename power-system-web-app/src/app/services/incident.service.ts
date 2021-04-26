import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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
}
