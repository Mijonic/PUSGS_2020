import { Component, OnInit, AfterViewInit, HostListener, ViewChild, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import * as L from 'leaflet';

const iconRetinaUrl = 'assets/marker-icon-2x.png';
const iconUrl = 'assets/marker-icon.png';
const shadowUrl = 'assets/marker-shadow.png';
const iconDefault = L.icon({
  iconRetinaUrl,
  iconUrl,
  shadowUrl,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  tooltipAnchor: [16, -28],
  shadowSize: [41, 41]
});
L.Marker.prototype.options.icon = iconDefault;

@Component({
  selector: 'app-work-map',
  templateUrl: './work-map.component.html',
  styleUrls: ['./work-map.component.css'],
})
export class WorkMapComponent implements OnInit, AfterViewInit {
  private map!: L.Map;
  public innerHeight: any;
  public containerHeight!:number;
  private hazardIcon:any;
  private crewIcon:any;
  ngZone:any;

  
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.innerHeight = window.innerHeight;
    this.containerHeight = this.innerHeight - 76;
  }

  constructor(private _router: Router, ngZone:NgZone) {
    this.ngZone = ngZone;
   } 

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    this.initMap();
    this.defineIcons();
    this.addMarkers();
    window.dispatchEvent(new Event('resize'));
  }

  private initMap(): void {
    this.map = L.map('map', {
      center: [ 44.2107675, 20.9224158],
      zoom: 8
    });

    const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });

    tiles.addTo(this.map); 
  }

  private defineIcons():void{
    let iconUrl = '../../assets/Images/crew-icon.png';
    let shadowUrl = 'assets/marker-shadow.png';
    this.crewIcon = L.icon({
      iconUrl,
      shadowUrl,
      iconSize: [41, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      tooltipAnchor: [16, -28],
      shadowSize: [41, 41]
    });

    iconUrl = '../../assets/Images/hazard-icon.png';
    shadowUrl = 'assets/marker-shadow.png';
    this.hazardIcon = L.icon({
      iconUrl,
      shadowUrl,
      iconSize: [41, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      tooltipAnchor: [16, -28],
      shadowSize: [41, 41]
    });
  }

  private addMarkers()
  {
    const circle = L.marker([45.2534882, 19.8335543], {icon:this.hazardIcon}).addTo(this.map);
    let marker = L.marker([45.2546674, 19.8209292], {icon:this.hazardIcon});
    marker.bindTooltip("Nesto se iskundacilo bato");
    marker.on('click', (e) => {
      this._router.navigate(['/']);
    });
    marker.addTo(this.map);  
    L.marker([45.257395, 19.801418], {icon:this.crewIcon}).addTo(this.map);    
  }

  private routeOnClick(url:string)
  {

  }

}
