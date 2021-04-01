import { TableControlOptions } from './../../shared/options/table-control-options.model';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-table-control',
  templateUrl: './table-control.component.html',
  styleUrls: ['./table-control.component.css']
})
export class TableControlComponent implements OnInit {
  @Input()
  tableControlOptions:TableControlOptions;

  constructor() { }

  ngOnInit(): void {
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.tableControlOptions.controlService.filter([filterValue]);
  }

  setFilterClasses()
  {
    let colW =  String(this.calculateCollumnWidth());
    if(this.tableControlOptions.shouldInitFilter)
    {
      let objreturn = {
        'col-md-12': colW == '12' ? true: false,
        'col-md-4': colW == '4' ? true: false,
        'col-md-6': colW == '6' ? true: false,
      };

      return objreturn;
    }else
    {
      return{
        'hidden':true
      };
    }

  }

  setRadioClasses()
  {
    let colW = String(this.calculateCollumnWidth());
    if(this.tableControlOptions.shouldInitRadio)
    {
      return{
        'col-md-12': colW == '12' ? true: false,
        'col-md-4': colW == '4' ? true: false,
        'col-md-6': colW == '6' ? true: false,
        'pt-3':true,
        'd-flex':true,
        'justify-content-center':true
      };
    }else
    {
      return{
        'hidden':'true'
      };
    }
  }

  setButtonClasses()
  {
    let colW = String(this.calculateCollumnWidth());
    if(this.tableControlOptions.shouldInitSaveButton)
    {
      return{
        'col-md-12': colW == '12' ? true: false,
        'col-md-4': colW == '4' ? true: false,
        'col-md-6': colW == '6' ? true: false,
        'pb-3':true,
        'd-flex':true,
        'justify-content-end':true
      };
    }else
    {
      return{
        'hidden':true
      };
    }
  }

  calculateCollumnWidth():number
  {
      let collumnsTotal = 12;
      let colCount = 0;
      if(this.tableControlOptions.shouldInitFilter)
        colCount++;
      if(this.tableControlOptions.shouldInitRadio)
        colCount++;
      if(this.tableControlOptions.shouldInitSaveButton)
        colCount++;

      if(colCount == 0)
        return 0;
      return collumnsTotal / colCount;
  }

  

}
