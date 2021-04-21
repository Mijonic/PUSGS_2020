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
}
