import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'app/shared/models/user.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  
  getAllUnassignedCrewMembers():Observable<User[]>{
    let requestUrl = environment.serverURL.concat("users/unassigned-crew-members");
    return this.http.get<User[]>(requestUrl);
  }
}
