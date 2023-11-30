import { Component } from '@angular/core';

import { FormControl, FormGroup, NgForm, Validators } from "@angular/forms";
import { AuthService } from '../auth.service';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs';
import { ReturnStatement } from '@angular/compiler';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {


  constructor(private authService: AuthService, private spinner: NgxSpinnerService, private router: Router) { }

  userName: string = "";
  password: string = "";
  userEmail: string = "";
  errorMessage:string = "";
  isPasswordValid:boolean = true;

  submitForm(submitNGForm: NgForm) {
    this.spinner.show();
    if (submitNGForm.touched == false) return;
    if (submitNGForm.valid == false) return;   

    this.authService.register(this.userName, this.userEmail, this.password).pipe(
      catchError(error => {
        console.error('Error:', error);
        this.spinner.hide();
        this.isPasswordValid = false;
        this.errorMessage = error?.error?.errorMessage[0];
        return [];
      })).subscribe((e) => {
      console.log(e); 
      this.spinner.hide();

      this.router.navigate(['/login']) });

  }

}
function subscribe(arg0: (e: any) => void) {
  throw new Error('Function not implemented.');
}

