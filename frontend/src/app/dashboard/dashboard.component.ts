import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Apiservice } from '../api.service';
import { FlightInfo } from '../models/flightInfo';
import { FlightData } from '../models/flightData';
import {
  faHouse,
  faUser,
  faArrowCircleRight, faArrowRight
} from '@fortawesome/free-solid-svg-icons';
import pilotProfile from '../models/pilotProfile';
import { UsercontextService } from '../usercontext.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements AfterViewInit, OnInit {
  constructor(
    private apiService: Apiservice,
    private userContext: UsercontextService
  ) {}

  ngOnInit(): void {
    this.user = this.userContext;
    this.userId = String(localStorage.getItem('userId')); 
    this.starters();
  }

  @ViewChild('flightCodeInput') flightCodeInput: any;

  faHouse = faHouse;
  faUser = faUser;
  faArrowCircleRight = faArrowCircleRight;
  faArrow = faArrowRight;

  flightInfo: FlightInfo[];
  flightInfoMaster: FlightInfo[];
  pilotProfile: pilotProfile[];
  selectedFlight: FlightInfo;
  selectedFlightData: FlightData;
  isShowDashboard = false;
  intervalID: any;
  selectedPilotId: any;
  user: any;
  userId: string ;

  pilotSelectHandler(item: any) {
    this.selectedPilotId = item;
    this.getFlights();
  }

  logout() {
    localStorage.removeItem('userToken');
    localStorage.removeItem('userName');
  }

  selectItem(item: FlightInfo) {
    if (this.isShowDashboard) {
      this.isShowDashboard = false;
    } else {
      this.isShowDashboard = true;
      this.selectedFlight = item;
    }
  }

  async starters() {
    this.getPilotProfile();
  }

  toogleSelectedFlight() {
    if (!this.selectedFlight == null) {
      this.isShowDashboard = true;
    } else {
      this.isShowDashboard = false;
    }
  }

  async getPilotProfile(): Promise<any> {
    this.apiService.getPilotProfile(String(this.userId) ).subscribe((data) => {
      this.pilotProfile = [...data.result];
      this.selectedPilotId = data.result[0].pilotId;
      this.getFlights();
      return data.result[0].pilotId;
    });
  }

  async filterFlights() {
    this.flightInfo = this.flightInfoMaster;
    const flightCode = this.flightCodeInput.nativeElement.value.toLowerCase();
    const flightCodeFiltered = this.flightInfo.filter((d) =>
      d.flightCode.toLowerCase().includes(flightCode)
    );
    this.flightInfo = [...flightCodeFiltered];
  }

  async getFlights() {
    setTimeout(() => {
      
      this.apiService
        .getFlightsList(String(this.selectedPilotId))
        .subscribe((data) => {
          this.flightInfo = data.result;
          this.flightInfo = [...this.flightInfo];
          this.flightInfoMaster = [...this.flightInfo];
        });
    }, 500);
  }

  ngAfterViewInit(): void {}
}
