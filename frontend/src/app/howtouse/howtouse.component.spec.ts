import { ComponentFixture, TestBed } from '@angular/core/testing';
 import { HowtouseComponent } from './howtouse.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterTestingModule } from '@angular/router/testing';

describe('HowtouseComponent', () => {
  let component: HowtouseComponent;
  let fixture: ComponentFixture<HowtouseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HowtouseComponent , NavbarComponent], imports :[FontAwesomeModule, RouterTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HowtouseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
