import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TabMessagingService implements OnDestroy{
  private subject = new Subject<any>();
  
  constructor() { }

  ngOnDestroy(): void {
    this.subject.unsubscribe();
  }

  showEdit(wrId:number)
  {
    this.subject.next(wrId);
  }

  getMessage():Observable<any>{
    return this.subject.asObservable();
  }

}
