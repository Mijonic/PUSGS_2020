import { GenericTableOptions } from './../../shared/options/generic-table-options.model';
import { WorkPlansControlService } from './../../services/work-plans-control.service';
import { TableControlOptions } from './../../shared/options/table-control-options.model';
import { Component, OnInit, AfterContentInit, ViewChild, AfterViewInit } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

export interface UserData {
  id: string;
  name: string;
  progress: string;
  color: string;
}

/** Constants used to fill up our data base. */
const COLORS: string[] = [
  'maroon', 'red', 'orange', 'yellow', 'olive', 'green', 'purple', 'fuchsia', 'lime', 'teal',
  'aqua', 'blue', 'navy', 'black', 'gray'
];
const NAMES: string[] = [
  'Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack', 'Charlotte', 'Theodore', 'Isla', 'Oliver',
  'Isabella', 'Jasper', 'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'
];

@Component({
  selector: 'app-work-plans',
  templateUrl: './work-plans.component.html',
  styleUrls: ['./work-plans.component.css'] 
})
export class WorkPlansComponent implements OnInit, AfterViewInit{
  displayedColumns: string[] = ['action', 'type', 'id',  'status', 'incident', 'startdate', 'enddate',  'createdby', 'company', 'phoneno', 'creationdate'];
  dataSource: MatTableDataSource<UserData>;
  tableControlOptions:TableControlOptions;
  genericTableOptions:GenericTableOptions<UserData>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild('sort') sort!: MatSort;


  constructor(private controlService:WorkPlansControlService) {
    // Create 100 users
    const users = Array.from({length: 100}, (_, k) => createNewUser(k + 1));

    this.dataSource = new MatTableDataSource(users);
    this.initTableControlOptions();
    this.initTableOptions();

  }
  ngAfterViewInit(): void {

    this.genericTableOptions.dataSource.paginator = this.paginator;
    this.genericTableOptions.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
  }

  initTableControlOptions()
  {
    this.tableControlOptions = {
      shouldInitFilter:true,
      shouldInitRadio:true,
      shouldInitSaveButton:true,
      shouldInitSearch:true,
      filterValues: ['Neki plan', 'Nekiji plan', 'Jos nekiji plan'],
      isMultiFilter:true,
      buttonNaviLink:'/',
      radioOptions:{
        value1:'all',
        value2:'mine',
        label1:'All',
        label2:'Mine'
      },
      controlService: this.controlService,
    }
  }

  initTableOptions(){
    this.genericTableOptions={
      columns:this.displayedColumns,
      dataSource:this.dataSource,
      editLink:'/',
      deleteService:this.controlService
  
    }
  
  }

}

/** Builds and returns a new User. */
function createNewUser(id: number): UserData {
  const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
      NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

  return {
    id: id.toString(),
    name: name,
    progress: Math.round(Math.random() * 100).toString(),
    color: COLORS[Math.round(Math.random() * (COLORS.length - 1))]
  };
}
