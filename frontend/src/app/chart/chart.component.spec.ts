import { ComponentFixture, TestBed, fakeAsync } from '@angular/core/testing';
import { HttpClient,  HttpClientModule, HttpHandler } from '@angular/common/http';
import { ChartComponent } from './chart.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { FlightInfo } from '../models/flightInfo';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


describe('ChartComponent', () => {
  let component: ChartComponent;
  let fixture: ComponentFixture<ChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChartComponent ],
      providers : [ HttpClient, HttpHandler],
      imports: [NgxChartsModule, HttpClientModule, BrowserAnimationsModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChartComponent);

    component = fixture.componentInstance;
    const flight : FlightInfo = {
      id: 0,
      flightCode: '',
      airliner: '',
      isActive: false,
      departureDateHour: new Date() ,
      arivalDateHour: new Date(),
      arivalAirportCode: '',
      departureAirportCode: ''
    }

    const isShowDashboard = false;

    component.selectedFlight = flight
    component.isShowDashboard = isShowDashboard;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getInitalData on ngOnChanges', () => {
    spyOn(component, 'getInitalData');
    
    component.ngOnChanges({});
    
    expect(component.getInitalData).toHaveBeenCalled();
  });

  it('should set isActive to true on ngOnInit if selectedFlight.isActive is true', () => {
    component.selectedFlight.isActive = true;

    component.ngOnInit();

    expect(component.isActive).toBe(true);
  });

  it('should set isActive to false on ngOnInit if selectedFlight.isActive is false', () => {
    component.selectedFlight.isActive = false;

    component.ngOnInit();

    expect(component.isActive).toBe(false);
  });

  it('should call getLiveData when onLive is called with toggleLiveData set to false', fakeAsync(() => {
    spyOn(component, 'getLiveData');

    component.onLive();

    expect(component.toggleLiveData).toBe(true);
    expect(component.getLiveData).toHaveBeenCalled();
  }));

  it('should clear interval when onLive is called with toggleLiveData set to true', fakeAsync(() => {
    spyOn(window, 'clearInterval');

    component.toggleLiveData = true;
    component.onLive();

    expect(component.toggleLiveData).toBe(false);
    expect(window.clearInterval).toHaveBeenCalledWith(component.internalID);
  }));
});
