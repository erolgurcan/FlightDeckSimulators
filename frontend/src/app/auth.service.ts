import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Apiservice } from './api.service';


@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor( private apiService: Apiservice) { }

  register(username:string, email:string, password: string):Observable<any>{
    return  this.apiService.register(username, email, password);
  }

  login (username: string, password: string):Observable<any>{
      return this.apiService.login(username, password);
  }

  authorize ( token: string | undefined ):Observable<any> {
    return this.apiService.authorize(token);
  }
}
