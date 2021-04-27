import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DisplayService {

  constructor(private datePipe:DatePipe) { }

  getBoolDisplay(bool:boolean)
  {
     if(bool)
      return "Yes";

      return "No";
  }

  getDateDisplay(date:Date)
  {
    return this.datePipe.transform(date, "dd/MM/yyyy");
  }
}
