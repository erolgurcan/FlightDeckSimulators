import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { MapComponent } from './map.component';
import { FlightInfo } from '../models/flightInfo';

describe('MapComponent', () => {
  let component: MapComponent;
  let fixture: ComponentFixture<MapComponent>;

  const flightInfo: FlightInfo ={
    id: 0,
    flightCode: '',
    airliner: '',
    isActive: false,
    departureDateHour: new Date(),
    arivalDateHour: new Date(),
    arivalAirportCode: '',
    departureAirportCode: ''
  }

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapComponent ], providers : [HttpClient, HttpHandler]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MapComponent);
    component = fixture.componentInstance;
    component.selectedFlight = flightInfo;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call initMap during ngAfterViewInit', () => {
    spyOn(component, 'initMap');
    component.ngAfterViewInit();
    expect(component.initMap).toHaveBeenCalled();
  });
});
