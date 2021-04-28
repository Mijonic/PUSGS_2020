import { HttpClient, HttpEvent, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Device } from 'app/shared/models/device.model';
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

  getById(id:number):Observable<WorkRequest>{
    let requestUrl = environment.serverURL.concat(`work-requests/${id}`);
    return this.http.get<WorkRequest>(requestUrl);
  }

  getAll():Observable<WorkRequest[]>{
    let requestUrl = environment.serverURL.concat(`work-requests`);
    return this.http.get<WorkRequest[]>(requestUrl);
  }

  getWorkRequestDevices(id:number):Observable<Device[]>{
    let requestUrl = environment.serverURL.concat(`work-requests/${id}/devices`);
    return this.http.get<Device[]>(requestUrl);
  }

  updateWorkRequest(workRequest:WorkRequest):Observable<WorkRequest>{
    let requestUrl = environment.serverURL.concat(`work-requests/${workRequest.id}`);
    return this.http.put<WorkRequest>(requestUrl, workRequest);
  }

  deleteWorkRequest(id:number):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`work-requests/${id}`);
    return this.http.delete<WorkRequest>(requestUrl);
  }

  uploadAttachment(file: File, workRequestId:number): Observable<HttpEvent<any>> {
    let requestUrl = environment.serverURL.concat(`work-requests/${workRequestId}/upload`);
    const formData: FormData = new FormData();

    formData.append('file', file);

    const request = new HttpRequest('POST', requestUrl, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(request);
  }
}
