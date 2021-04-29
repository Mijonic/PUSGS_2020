import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DeviceService } from 'app/services/device.service';
import { Device } from 'app/shared/models/device.model';
import { ToastrService } from 'ngx-toastr';
import { Location } from 'app/shared/models/location.model'; 
import { LocationService } from 'app/services/location.service';



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
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.css']
})


export class DevicesComponent implements OnInit {

  displayedColumns: string[] = ['action', 'id', 'name', 'type', 'coordinates', 'address', 'map'];
  dataSource: MatTableDataSource<Device>;
  isLoading:boolean = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  devices:Device[] = [];
  allDevices:Device[] = [];
  //allLocations:Location[] = [];

  
 
  toppings = new FormControl();
  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato']; 


  



  
 
 

  constructor(private deviceService:DeviceService,  private toastr: ToastrService) {


  }

  ngOnInit(): void {
    window.dispatchEvent(new Event('resize'));
    this.getDevices();
    
    
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

 
  getDevices()
  {
    this.deviceService.getAllDevices().subscribe(
      data =>{
        this.allDevices = data;
        this.devices = data;
        this.dataSource = new MatTableDataSource(data);
        this.isLoading = false;
       
      },
      error =>{
        this.getDevices();
      }
    )
  }

  getAddressFromLocation(location: Location) {
        
    return  `${location.street}, ${location.city}, ${location.zip}`

  }

  // getDeviceTypeString(deviceType:number)
  // {
  //     switch(deviceType)
  //     {
  //         case 0: return "POWER_SWITCH";
  //         case 1: return "FUSE"
  //         case 2: return "TRANSFORMER"
  //         case 3: return "DISCONNECTOR"   
  //     }

  //     return "UNKNOWN";
  // }

  delete(deviceId: number)
  {
   
    this.deviceService.deleteDevice(deviceId).subscribe(x =>{
        this.getDevices();
        this.toastr.success("Device successfully deleted","", {positionClass: 'toast-bottom-left'});
    });
  }



 

}


