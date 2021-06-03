import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SafetyDocument } from 'app/shared/models/safety-document.model';
import { StateChange } from 'app/shared/models/state-change.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SafetyDocumentService {

  constructor(private http: HttpClient) { }
  
  getAllSafetyDocuments():Observable<SafetyDocument[]>{
    let requestUrl = environment.serverURL.concat("safety-documents");
    return this.http.get<SafetyDocument[]>(requestUrl);
  }

  getSafetyDocumentById(id:number):Observable<SafetyDocument>{
    let requestUrl = environment.serverURL.concat(`safety-documents/${id}`);
    return this.http.get<SafetyDocument>(requestUrl);
  }

  createNewSafetyDocument(safetyDocument:SafetyDocument):Observable<SafetyDocument>{
    
    let requestUrl = environment.serverURL.concat("safety-documents");
    console.log("slanje ka serveru")
    console.log(requestUrl);
    console.log(safetyDocument);
    return this.http.post<SafetyDocument>(requestUrl, safetyDocument);
  }

  updateSafetyDocument(safetyDocument: SafetyDocument):Observable<SafetyDocument>{
    let requestUrl = environment.serverURL.concat(`safety-documents/${safetyDocument.id}`);
    return this.http.put<SafetyDocument>(requestUrl, safetyDocument);
  }

  getCrewForSafetyDocument(id:number):Observable<SafetyDocument>{
    let requestUrl = environment.serverURL.concat(`safety-documents/${id}/crew`);
    return this.http.get<SafetyDocument>(requestUrl);
  }

  getStateChanges(wrId:number): Observable<StateChange[]> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${wrId}/state-changes`);
		return this.http.get<StateChange[]>(requestUrl);
  }

  approveSafetyDocument(wrId:number): Observable<SafetyDocument> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${wrId}/approve`);
		return this.http.put<SafetyDocument>(requestUrl, {});
  }

  
  denySafetyDocument(wrId:number): Observable<SafetyDocument> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${wrId}/deny`);
		return this.http.put<SafetyDocument>(requestUrl, {});
  }

  
  cancelSafetyDocument(wrId:number): Observable<SafetyDocument> {
    let requestUrl = environment.serverURL.concat(`safety-documents/${wrId}/cancel`);
		return this.http.put<SafetyDocument>(requestUrl, {});
  }

 
}
