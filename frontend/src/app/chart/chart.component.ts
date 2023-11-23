import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Apiservice } from '../api.service';
import { LegendPosition, ColorHelper } from '@swimlane/ngx-charts';
import { FlightInfo } from '../models/flightInfo';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css'],
})
export class ChartComponent implements OnInit, OnChanges {
  @Input() selectedFlight: FlightInfo;
  @Input() isShowDashboard: boolean;

  constructor(private apiService: Apiservice) {}

  ngOnChanges(changes: SimpleChanges): void {
    clearInterval(this.internalID);
    this.getInitalData();
  }

  ngOnInit(): void {
    this.isActive = this.selectedFlight.isActive ;
  }

  isActive: boolean;
  toggleLiveData = false;
  internalID: any;
  isLiveData = false;

  public legendPosition: LegendPosition = LegendPosition.Below;

  single = [
    {
      name: 'Outside Temperature',
      value: 0,
    },
    {
      name: 'Remanining Fuel',
      value: 0,
    },
    {
      name: 'Local Time',
      value: "",
    },
    {
      name: 'Landing Gear',
      value: 1,
    },
    {
      name: 'Angle of Attack',
      value: 0,
    },
    {
      name: 'Extra Data ',
      value: 0,
    },
  ];

  dataset = [
    {
      name: 'Altitude',
      series: [
        {
          value: 0,
          name: '',
        },
      ],
    },
  ];

  datasetSpeed = [
    {
      name: 'Speed',
      series: [
        {
          value: 0,
          name: '',
        },
      ],
    },
  ];


  engine = [
    {
      name: 'Engine N1 Throttle',
      value: 0,
    },
    {
      name: 'Engine N2 Throttle',
      value: 0,
    },
  ];

  speed = [
    {
      name: 'Ground Speed',
      value: 0,
    },
    {
      name: 'True Air Speed',
      value: 0,
    },
  ];

  onLive(): void {
    if (this.toggleLiveData == false) {
      this.toggleLiveData = true;
      this.getLiveData();
    } else {
      this.toggleLiveData = false;
      clearInterval(this.internalID);
    }
  }

  getInitalData(): void {
    this.dataset[0].series = [];
    this.datasetSpeed[0].series = [];
    this.apiService
      .getFlightData('5', this.selectedFlight.flightCode)
      .subscribe((data) => {
        console.log(data);

        for (let i = 0; i < data.length; i++) {
          this.dataset[0].series.push({
            value: Number(data[i].altitude),
            name: new Date(data[i].loggingTime).toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true }),
          });
          this.datasetSpeed[0].series.push( {
            value: Number(data[i].trueSpeed),
            name: new Date(data[i].loggingTime).toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true }),
          } )
        }

        this.dataset = [...this.dataset];
        this.datasetSpeed = [...this.datasetSpeed];
        console.log(this.dataset);
      });
  }

  getLiveData(): void {
    this.dataset[0].series = [];
    this.internalID = setInterval(() => {
      this.apiService.getLatestData(this.selectedFlight).subscribe((data) => {
        this.engine[0].value = data.result.throttle1;
        this.engine[1].value = data.result.throttle2;
        this.engine = [...this.engine];

        this.speed[0].value = Number(Number(data.result.groundSpeed).toPrecision(3));
        this.speed[1].value = Number(Number(data.result.trueAirSpeed).toPrecision(3))
        this.speed = [...this.speed];

        this.single[0].value = data.result.outsideTemperature;
        this.single[1].value =Number(Number(data.result.totalFuel).toPrecision(3));
        this.single[2].value = new Date(data.result.loggingTime).getHours() + " : " + new Date(data.result.loggingTime).getMinutes()  ;
        this.single[3].value = data.result.landingGear === 0 ? "Up" : "Down"
        this.single[4].value = Number(data.result.aoa).toPrecision(2)
        this.single = [...this.single];

        console.log(data.result)

        if (this.dataset[0].series.length > 50) {
          this.dataset[0].series.shift();
          this.dataset[0].series.push({
            value: Number(data.result.altitude),
            name: String(data.result.loggingTime),
          });
        } else {
          this.dataset[0].series.push({
            value: Number(data.result.altitude),
            name: String(data.result.loggingTime),
          });
        }
        this.dataset = [...this.dataset];
      });
    }, 5000);
  }
}
