import { Observable } from "rxjs";

export interface DeleteService {
    delete(id:string):Observable<any>;
}