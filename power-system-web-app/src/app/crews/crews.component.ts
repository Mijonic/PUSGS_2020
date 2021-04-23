import { Crew } from 'app/shared/models/crew.model';
import { CrewService } from './../services/crew.service';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

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
    this.isLoading = true;
    this.getCrews();
  }

  ngAfterViewInit() {
 
  }

  getCrews(){
    this.crewService.getAllCrews().subscribe(
      data => {
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.paginator = this.paginator; 
      this.isLoading = false;
    },
    error =>{
      this.getCrews();
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


