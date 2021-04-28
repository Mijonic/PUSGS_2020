import { ToastrService } from 'ngx-toastr';
import { WorkRequestService } from './../../../../services/work-request.service';
import { DeviceService } from 'app/services/device.service';
import { Device } from './../../../../shared/models/device.model';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ChooseEquipmentDialogComponent } from 'app/documents/dialogs/choose-equipment-dialog/choose-equipment-dialog.component';
import { ActivatedRoute } from '@angular/router';
import { TabMessagingService } from 'app/services/tab-messaging.service';


@Component({
  selector: 'app-work-request-equipment',
  templateUrl: './work-request-equipment.component.html',
  styleUrls: ['./work-request-equipment.component.css']
})
export class WorkRequestEquipmentComponent implements OnInit, AfterViewInit{

  displayedColumns: string[] = ['id', 'name', 'type', 'coordinates', 'address', 'map', 'remove'];
  dataSource: MatTableDataSource<Device>;
  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(public dialog:MatDialog, private wrService:WorkRequestService, private route:ActivatedRoute, private toastr:ToastrService,
    private tabMessaging:TabMessagingService) {
  }

  ngOnInit(): void {
    const wrId = this.route.snapshot.paramMap.get('id');
    this.loadDevices(+wrId!);
    this.tabMessaging.showEdit(+wrId!);
  }

  loadDevices(id:number)
  {
    this.isLoading = true;
    this.wrService.getWorkRequestDevices(id).subscribe(
      data =>{
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoading = false;
      },
      error =>{
        if(error.error instanceof ProgressEvent)
          {
            this.loadDevices(id);
          }else
          {
            this.toastr.error(error.error);
          }
      }
    )
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



  onAddDevice()
  {
    const dialogRef = this.dialog.open(ChooseEquipmentDialogComponent, {width: "70%"});

    dialogRef.afterClosed().subscribe((result: any) => {
      console.log(`The dialog was closed and choosen id is ${result}`);
    });
  }

}


