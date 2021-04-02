import { DeleteService } from './../shared/interfaces/delete-service';
import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { SearchFilterService } from './../shared/interfaces/search-filter-service';

@Injectable({
    providedIn: 'root'
  })
export class WorkPlansControlService implements  SearchFilterService, DeleteService{
   
    delete(id: string): Observable<any> {
        return of(`Zvao si me da obrisem ${id}`);
    }
    search(input: string): Observable<any> {
        return of(`Zvao si me da potrazim ${input}`);
    }
    filter(input: string[]): Observable<any> {
        return of(`Zvao si me da filtriram ${input}`);
    }

}