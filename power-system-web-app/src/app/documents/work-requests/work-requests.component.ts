import { UserService } from './../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { DisplayService } from './../../services/display.service';
import { WorkRequestService } from './../../services/work-request.service';
import { WorkRequest } from './../../shared/models/work-request.model';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource} from '@angular/material/table';


@Component({
  selector: 'app-work-requests',
  templateUrl: './work-requests.component.html',
  styleUrls: ['./work-requests.component.css']
})
export class WorkRequestsComponent implements  AfterViewInit {
  displayedColumns: string[] = ['action', 'id', 'type', 'status', 'incident', 'street', 'startdate', 'enddate', 'createdby', 'emergency','company', 'phoneno', 'creationdate'];
  dataSource: MatTableDataSource<WorkRequest>;
  toppings = new FormControl();
  documentStatuses: any[] = 
  [ {status:'All', value:'all'},
    {status:'Draft', value:'draft'},
    {status:'Canceled', value:"canceled"},
    {status:'Approved', value:'approved'},
    {status:'Denied', value:'denied'},
    ];
  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private workRequestService:WorkRequestService, public display:DisplayService, private toastr:ToastrService,
    private userService:UserService) {
  }

  loadWorkRequests()
  {
    this.workRequestService.getAll().subscribe(
      data =>{
          this.dataSource = new MatTableDataSource(data);
          this.isLoading = false;
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
      }, 
      error =>{
        this.loadWorkRequests();
      }
    )
  }

  ngOnInit(): void {
    window.dispatchEvent(new Event('resize'));
    this.loadWorkRequests();
  }

  ngAfterViewInit() {

  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }

  }

  delete(id:number)
  {
    this.workRequestService.deleteWorkRequest(id).subscribe(
      data =>{
        this.loadWorkRequests();
        this.toastr.success("Work request successfully deleted.");
        this.toastr.info("All media attached to this work request is also deleted.");
      },
      error =>{
        if(error.error instanceof ProgressEvent)
                {
                  this.toastr.error("Server is unreachable");
                }else
                {
                  this.toastr.error(error.error);
                }
      }
    );
  }
}

