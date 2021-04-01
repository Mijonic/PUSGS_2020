import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { SearchFilterService } from './../shared/interfaces/search-filter-service';

@Injectable({
    providedIn: 'root'
  })
export class WorkPlansControlService implements  SearchFilterService{
    search(input: string): Observable<any> {
        return of(`Zvao si me da potrazim ${input}`);
    }
    filter(input: string[]): Observable<any> {
        return of(`Zvao si me da filtriram ${input}`);
    }

}