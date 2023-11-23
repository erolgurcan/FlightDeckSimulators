import { Injectable } from '@angular/core';
import userModel from './models/userModel';

@Injectable({
  providedIn: 'root',
})
export class UsercontextService {
  
  user: any;

  constructor() {} 

  addUser(user:userModel) {
    this.user = user
  }

  getUser(): userModel[] {
    return this.user;
  }
}
