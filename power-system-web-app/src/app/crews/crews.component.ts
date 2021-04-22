import { Crew } from 'app/shared/models/crew.model';
import { CrewService } from './../services/crew.service';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

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
  selector: 'app-crews',
  templateUrl: './crews.component.html',
  styleUrls: ['./crews.component.css']
})
export class CrewsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['action', 'name', 'members'];
  dataSource: MatTableDataSource<Crew>;

  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private crewService:CrewService) {

  }

  ngOnInit(): void {
    this.getCrews();
  }

  ngAfterViewInit() {
 
  }

  getCrews(){
    this.crewService.getAllCrews().subscribe(x => {
      this.dataSource = new MatTableDataSource(x);
      this.dataSource.paginator = this.paginator; 
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  delete(crewId:number)
  {
    this.crewService.deleteCrew(crewId).subscribe(x =>{
        this.getCrews();
    });
  }
}


