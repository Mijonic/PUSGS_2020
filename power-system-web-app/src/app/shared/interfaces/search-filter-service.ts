import { Observable } from "rxjs";

export interface SearchFilterService {
    search(input:string):Observable<any>;
    filter(input:string[]):Observable<any>;
}
