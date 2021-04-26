import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WorkRequest } from 'app/shared/models/work-request.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkRequestService {

  constructor(private http: HttpClient) { }
  
  createWorkRequest(workRequest:WorkRequest):Observable<WorkRequest>{
    let requestUrl = environment.serverURL.concat("work-requests");
    return this.http.post<WorkRequest>(requestUrl, workRequest);
  }
}
