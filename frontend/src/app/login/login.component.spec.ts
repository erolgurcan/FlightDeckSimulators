import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClient, HttpHandler, HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { LoginComponent } from './login.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { FormsModule, NgForm } from '@angular/forms';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AuthService } from '../auth.service';
import { of } from 'rxjs';


describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: AuthService;
  let spinnerService: NgxSpinnerService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginComponent, NavbarComponent ], providers:[HttpClient, HttpHandler],
      imports: [ FormsModule, FontAwesomeModule, HttpClientModule, RouterTestingModule, NgxSpinnerModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    authService = TestBed.inject(AuthService);
    spinnerService = TestBed.inject(NgxSpinnerService);
    
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form controls', () => {
    // Ensure that form controls are initialized
    expect(component.userName).toBe('');
    expect(component.password).toBe('');
  });

  it('should display error message on invalid login', fakeAsync(() => {
    spyOn(authService, 'login').and.returnValue(of({ isSuccess: false }));
    spyOn(spinnerService, 'show');
    spyOn(spinnerService, 'hide');
    const form: NgForm = { touched: true, valid: true } as NgForm;

    component.submitForm(form);
    tick(); // Wait for async operations

    expect(spinnerService.show).toHaveBeenCalled();
    expect(spinnerService.hide).toHaveBeenCalled();
    expect(component.invalidCredentials).toBe(true);
  }));
 
});
