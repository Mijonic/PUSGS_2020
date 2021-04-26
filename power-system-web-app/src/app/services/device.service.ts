import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Device } from 'app/shared/models/device.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  constructor(private http: HttpClient) { }
  
  getAllDevices():Observable<Device[]>{
    let requestUrl = environment.serverURL.concat("devices");
    return this.http.get<Device[]>(requestUrl);
  }

  getDeviceById(id:number):Observable<Device>{
    let requestUrl = environment.serverURL.concat(`devices/${id}`);
    return this.http.get<Device>(requestUrl);
  }

  createNewDevice(device:Device):Observable<Device>{
    console.log(device)
    let requestUrl = environment.serverURL.concat("devices");
    return this.http.post<Device>(requestUrl, device);
  }

  updateDevice(device: Device):Observable<Device>{
    let requestUrl = environment.serverURL.concat(`devices/${device.id}`);
    return this.http.put<Device>(requestUrl, device);
  }

  deleteDevice(id:number):Observable<{}>{
    let requestUrl = environment.serverURL.concat(`devices/${id}`);
    return this.http.delete(requestUrl);
  }

}
