import { Component } from '@angular/core';

import { FormControl, FormGroup, NgForm, Validators } from "@angular/forms";
import { AuthService } from '../auth.service';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';


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

  submitForm(submitNGForm: NgForm) {
    this.spinner.show();
    if (submitNGForm.touched == false) return;
    if (submitNGForm.valid == false) return;

    this.authService.login(this.userName, this.password).subscribe(() => { 
      this.spinner.hide();
      this.router.navigate(['/dashboard']) });

  }

}
