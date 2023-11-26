import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import {
  HttpClient,
  HttpClientModule,
  HttpHandler,
} from '@angular/common/http';
import { DashboardComponent } from './dashboard.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterTestingModule } from '@angular/router/testing';
import { Apiservice } from '../api.service';
import { of } from 'rxjs';
import { FlightInfo } from '../models/flightInfo';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let apiService: Apiservice;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DashboardComponent],
      providers: [HttpClient, HttpHandler],
      imports: [HttpClientModule, RouterTestingModule, FontAwesomeModule],
    }).compileComponents();

    apiService = TestBed.inject(Apiservice);

    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call starters method on component initialization', () => {
    spyOn(component, 'starters');
    component.ngOnInit();
    expect(component.starters).toHaveBeenCalled();
  });

  it('should call getPilotProfile and getFlights when starters is called', fakeAsync(() => {
    spyOn(component, 'getFlights');

    const result: any = {
      result: [
        {
          pilotId: 1,
        },
      ],
    };
    spyOn(apiService, 'getPilotProfile').and.returnValue(of(result));

    component.starters();
    tick();

    expect(apiService.getPilotProfile).toHaveBeenCalledWith(component.userId);
    expect(component.getFlights).toHaveBeenCalled();
  }));

  it('should set isShowDashboard to true when selectItem is called', () => {
    const fakeFlight: FlightInfo = {
      id: 0,
      flightCode: '',
      airliner: '',
      isActive: false,
      departureDateHour: new Date(),
      arivalDateHour: new Date(),
      arivalAirportCode: '',
      departureAirportCode: ''
    };
    component.selectItem(fakeFlight);
    expect(component.isShowDashboard).toBeTruthy();
    expect(component.selectedFlight).toBe(fakeFlight);
  });

  it('should set isShowDashboard to false when selectItem is called and isShowDashboard is true', () => {
    component.isShowDashboard = true;
    component.selectItem({} as FlightInfo);
    expect(component.isShowDashboard).toBeFalsy();
  });

  it('should call getFlights when pilotSelectHandler is called', () => {
    spyOn(component, 'getFlights');
    
    const fakeItem: any = { /* create a fake item */ };
    component.pilotSelectHandler(fakeItem);

    expect(component.selectedPilotId).toBe(fakeItem);
    expect(component.getFlights).toHaveBeenCalled();
  });
});
