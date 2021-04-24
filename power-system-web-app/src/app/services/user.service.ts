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

  getAllUsers():Observable<User[]>{
    let requestUrl = environment.serverURL.concat("users");
    return this.http.get<User[]>(requestUrl);
  }

  approveUser(id:number):Observable<User>{
    let requestUrl = environment.serverURL.concat(`users/${id}/approve`);
    return this.http.put<User>(requestUrl, {});
  }

  denyUser(id:number):Observable<User>{
    let requestUrl = environment.serverURL.concat(`users/${id}/deny`);
    return this.http.put<User>(requestUrl, {});
  }

  createUser(user:User):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`users`);
    return this.http.post<User>(requestUrl, user);
  }

}
