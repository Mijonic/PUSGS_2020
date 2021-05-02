import { IMultimediaService } from './../shared/interfaces/multimedia-service';
import { MultimediaAttachment } from './../shared/models/multimedia-attachment.model';
import { HttpClient, HttpEvent, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Device } from 'app/shared/models/device.model';
import { WorkRequest } from 'app/shared/models/work-request.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkRequestService implements IMultimediaService {

  constructor(private http: HttpClient) { }
  
  deleteAttachment(filename: string, documentId: number): Observable<any> {
    let requestUrl = environment.serverURL.concat(`work-requests/${documentId}/attachments/${filename}`);
    return this.http.delete(requestUrl);
  }
  
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
    let requestUrl = environment.serverURL.concat(`work-requests/${workRequestId}/attachments`);
    const formData: FormData = new FormData();

    formData.append('file', file);

    const request = new HttpRequest('POST', requestUrl, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(request);
  }

  downloadAttachment(wrId:number, filename:string): Observable<any> {
    let requestUrl = environment.serverURL.concat(`work-requests/${wrId}/attachments/${filename}`);
		return this.http.get(requestUrl, {responseType: 'blob'});
  }

  getAttachments(wrId:number): Observable<MultimediaAttachment[]> {
    let requestUrl = environment.serverURL.concat(`work-requests/${wrId}/attachments`);
		return this.http.get<MultimediaAttachment[]>(requestUrl);
  }


}
