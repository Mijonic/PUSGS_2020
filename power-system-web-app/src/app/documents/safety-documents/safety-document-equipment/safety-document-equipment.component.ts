import { ChooseEquipmentDialogComponent } from './../../dialogs/choose-equipment-dialog/choose-equipment-dialog.component';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ValidationService } from 'app/services/validation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { TabMessagingService } from 'app/services/tab-messaging.service';

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
  selector: 'app-safety-document-equipment',
  templateUrl: './safety-document-equipment.component.html',
  styleUrls: ['./safety-document-equipment.component.css']
})
export class SafetyDocumentEquipmentComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'type', 'coordinates', 'address', 'map', 'remove'];
  dataSource: MatTableDataSource<UserData>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(public dialog:MatDialog, private validation:ValidationService, private route:ActivatedRoute, private userService:UserService,
    private toastr:ToastrService, private router:Router,
   private tabMessaging:TabMessagingService) {
    // Create 100 users
    const users = Array.from({length: 100}, (_, k) => createNewUser(k + 1));

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
  }

  ngOnInit(): void {
    const safetyDocumentId = this.route.snapshot.paramMap.get('id');
    //this.loadDevices(+wrId!);
    this.tabMessaging.showEdit(+safetyDocumentId!);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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



