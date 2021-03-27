import { Component, OnInit, AfterViewInit, HostListener, ViewChild } from '@angular/core';
import * as L from 'leaflet';

@Component({
  selector: 'app-work-map',
  templateUrl: './work-map.component.html',
  styleUrls: ['./work-map.component.css'],
})
export class WorkMapComponent implements OnInit, AfterViewInit {
  private map!: L.Map;
  public innerHeight: any;
  public containerHeight!:number;

  
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.innerHeight = window.innerHeight;
    this.containerHeight = this.innerHeight - 76;
  }

  constructor() { } 

  ngAfterViewInit(): void {
    
  }

  ngOnInit(): void {
    this.initMap();
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

}
