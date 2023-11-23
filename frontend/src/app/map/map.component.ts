import {
  AfterViewInit,
  Component,
  Input,
  OnDestroy,
  OnInit,

} from '@angular/core';
import * as L from 'leaflet';
import 'leaflet-rotatedmarker';
import { Apiservice } from '../api.service';
import { FlightInfo } from '../models/flightInfo';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
})
export class MapComponent implements AfterViewInit, OnInit, OnDestroy {
  @Input() selectedFlight: FlightInfo;
  @Input() isShowDashboard: boolean;

  private map: any;

  constructor(private apiService: Apiservice) {}

  ngOnDestroy(): void {
    clearInterval(this.intervalID);
  }
  items = [];
  intervalID: any;
  result: any[];
  latlngs: any[];


  ngAfterViewInit(): void {
    this.initMap();
    
  }

  ngOnInit(): void {
  }


  baseLayers : any;


  private initMap(): void {
    this.map = L.map('map', {
      center: [39.8282, -98.5795],
      zoom: 4,
    });


    const tiles = L.tileLayer(
      'https://tile.openstreetmap.org/{z}/{x}/{y}.png',
      {
        maxZoom: 18,
        minZoom: 4,
        attribution:
          '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
      }
    );
    tiles.addTo(this.map);
    var myIcon = L.icon({
      iconUrl: '../../assets/airplane.png',
    });

    const marker = L.marker([51.5, -0.09], { icon: myIcon });
    marker.bindPopup( `<p>Flight Code  ${this.selectedFlight.flightCode}</p><p>Flight Code</p>` ).openPopup();
    marker.addTo(this.map);

    if (this.selectedFlight.isActive) {
      this.intervalID = setInterval(() => {
        this.apiService
          .getLatestData(this.selectedFlight)
          .subscribe((response) => {
            marker
              .setLatLng([response.result.latitude, response.result.longitude])
              .setRotationAngle(response.result.heading ).setRotationOrigin("left");
              let emptyArray = this.latlngs;
              emptyArray.push([response.result.latitude, response.result.longitude]);
              this.latlngs = [...emptyArray];
              L.polyline(this.latlngs, { color: 'blue' }).addTo(this.map);
          });
      }, 5000);
    }

    this.apiService
      .getFlightData('5', this.selectedFlight.flightCode)
      .subscribe((data) => {
        let emptyArray = [];
        for (let i = 0; i < data.length; i++) {
          emptyArray.push([data[i].latitude, data[i].longitude]);
        }
        this.latlngs = [...emptyArray];

        L.polyline(this.latlngs, { color: 'red' }).addTo(this.map);
      });

      
  }
}
