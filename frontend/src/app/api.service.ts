import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Axios } from 'axios';
import { FlightInfo } from './models/flightInfo';
import { local } from 'd3';

@Injectable({
  providedIn: 'root',
})
export class Apiservice {

  token:string  = "";
  constructor(private httpClient: HttpClient) {
    this.token =  localStorage.getItem('userToken')==undefined? String( localStorage.getItem('userToken') ) : "" ;
    console.log(this.token);    
  }


  headers = {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
    'Authorization': this.token
  };

  baseUrl = 'https://flight-data-server.azurewebsites.net';
  //baseUrl = 'https://localhost:7152';

  authorize(token: string | undefined): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}/api/UsersAuth/authorize`, {
      headers: this.headers,
    });
  }

  register(userName: string, userEmail: string, password: string) {
    return this.httpClient.post(
      `${this.baseUrl}/api/UsersAuth/register`,
      {
        username: userName,
        useremail: userEmail,
        role: 'USER',
        name: userName,
        password: password,
      },
      {
        headers: this.headers,
      }
    );
  }

  login(username: string, password: string): Observable<any> {
   return this.httpClient.post(
      `${this.baseUrl}/api/UsersAuth/login`,
      {
        username: username,
        password: password,
      },
      { headers: this.headers }
    );

  }

  saveToken(token: string): void {
    localStorage.setItem('TOKEN', token);
  }

  getLatestData(selectedFlight: FlightInfo): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}/api/FlightData/get/latest`, {
      headers: { flightCode: selectedFlight.flightCode, Authorization: String(localStorage.getItem('userToken'))},
    });
  }

  getFlightsList(pilotID: string): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}/api/FlightData/getFlightList`, {
      headers: { pilotID: pilotID, Authorization: String(localStorage.getItem('userToken'))},
    });
  }

  getFlightData(pilotID: string, flightCode: string): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}/api/FlightData/filter`, {
      headers: { pilotID: pilotID, flightCode: flightCode, Authorization: String(localStorage.getItem('userToken')) },
    });
  }

  getPilotProfile(userID: string): Observable<any> {
    return this.httpClient.get(
      `${this.baseUrl}/api/UsersAuth/getPilotProfile`,
      {
        headers: {
          userID: userID, Authorization: String(localStorage.getItem('userToken'))
        },
      }
    );
  }
}
