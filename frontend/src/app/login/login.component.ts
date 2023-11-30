import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { UsercontextService } from '../usercontext.service';
import userModel from '../models/userModel'
import { Injector } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  userName: string = '';
  password: string = '';
  isLoginSucceeded: boolean;
  isRouting: boolean = false;
  progressBar: any;
  user: userModel;
  invalidCredentials: boolean=  false;


  constructor(
    private authService: AuthService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private userContext: UsercontextService
  ) {}

  ngOnInit() {
    this.authorize();
  }

  authorize() {
    if (localStorage.getItem('userToken')  ) {
      this.isRouting = true;
      setTimeout(() => {

        this.router.navigate(['dashboard']);
      }, 4000);
    }
  }

  submitForm(submitNGForm: NgForm) {
    this.spinner.show();
    if (submitNGForm.touched == false) return;
    if (submitNGForm.valid == false) return;

    this.authService.login(this.userName, this.password).subscribe((d) => {
      this.spinner.hide();
      if ( d.isSuccess != true ){
        this.invalidCredentials = true;
      }else {
        this.user = d.result.user;
        this.userContext.addUser(this.user);
        localStorage.setItem('userToken', d.result.token);
        localStorage.setItem('userId',d.result.user.userId );
        localStorage.setItem('userName',d.result.user.userName );
        this.router.navigate(['/dashboard']);  
      }
    
    });

  }
}
