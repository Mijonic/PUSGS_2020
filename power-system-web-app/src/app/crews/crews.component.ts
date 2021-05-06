import { MatSort } from '@angular/material/sort';
import { Crew } from 'app/shared/models/crew.model';
import { CrewService } from './../services/crew.service';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { merge, Observable, of } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-crews',
  templateUrl: './crews.component.html',
  styleUrls: ['./crews.component.css']
})
export class CrewsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['action', 'crewName', 'members'];
  dataSource: MatTableDataSource<Crew>;
  filteredAndPagedCrews: Observable<Crew[]>;
  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private crewService:CrewService) {

  }

  ngOnInit(): void {
  }

  ngAfterViewInit() {

    this.filteredAndPagedCrews = merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoading = true;
          return this.crewService.getCrewsPaged(
            this.sort.active, this.sort.direction, this.paginator.pageIndex, this.paginator.pageSize);
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoading = false;
          this.paginator.length = data.totalCount;

          return data.crews;
        }),
        catchError(() => {
          this.isLoading = false;
          return of([]);
        })
      );
  }

 /* getCrews(){
    this.crewService.getAllCrews().subscribe(
      data => {
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.paginator = this.paginator; 
      this.isLoading = false;
    },
    error =>{
      this.getCrews();
    });

  }*/

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
        //this.getCrews();
    });
  }

  resetPaging(): void {
    this.paginator.pageIndex = 0;
  }

}


