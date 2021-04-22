import { Crew } from 'app/shared/models/crew.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CrewService {

  constructor(private http: HttpClient) { }

  getAllCrews():Observable<Crew[]>{
    let requestUrl = environment.serverURL.concat("crews");
    return this.http.get<Crew[]>(requestUrl);
  }

  getCrewById(id:number):Observable<Crew>{
    let requestUrl = environment.serverURL.concat(`crews/${id}`);
    return this.http.get<Crew>(requestUrl);
  }

  createNewCrew(crew:Crew):Observable<Crew>{
    let requestUrl = environment.serverURL.concat("crews");
    return this.http.post<Crew>(requestUrl, crew);
  }

  updateCrew(crew:Crew):Observable<Crew>{
    let requestUrl = environment.serverURL.concat(`crews/${crew.id}`);
    return this.http.put<Crew>(requestUrl, crew);
  }

  deleteCrew(id:number):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`crews/${id}`);
    return this.http.delete(requestUrl);
  }
}
