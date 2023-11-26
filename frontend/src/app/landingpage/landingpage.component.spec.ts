import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NavbarComponent } from '../navbar/navbar.component';
import { LandingpageComponent } from './landingpage.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RouterTestingModule } from '@angular/router/testing';

describe('LandingpageComponent', () => {
  let component: LandingpageComponent;
  let fixture: ComponentFixture<LandingpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandingpageComponent, NavbarComponent ],
      imports:[ FontAwesomeModule, RouterTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandingpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
