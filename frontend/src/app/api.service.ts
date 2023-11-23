import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Axios } from 'axios';
import { FlightInfo } from './models/flightInfo';

@Injectable({
  providedIn: 'root',
})
export class Apiservice {
  constructor(private httpClient: HttpClient) {}

  headers = {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
  };

  authorize(token: string | undefined): Observable<any> {
    return this.httpClient.get(
      'https://flight-data-server.azurewebsites.net/api/UsersAuth/authorize',
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
  }

  login(username: string, password: string): Observable<any> {
    return this.httpClient.post(
      'https://flight-data-server.azurewebsites.net/api/UsersAuth/login',
      {
        username: username,
        password: password,
      },
      { headers: this.headers }
    );
  }

  getLatestData(selectedFlight: FlightInfo): Observable<any> {
    return this.httpClient.get(
      'https://flight-data-server.azurewebsites.net/api/FlightData/get/latest',
      {
        headers: { flightCode: selectedFlight.flightCode },
      }
    );
  }

  getFlightsList(pilotID: string): Observable<any> {
    return this.httpClient.get(
      'https://flight-data-server.azurewebsites.net/api/FlightData/getFlightList',
      {
        headers: { pilotID: pilotID },
      }
    );
  }

  getFlightData(pilotID: string, flightCode: string): Observable<any> {
    return this.httpClient.get(
      'https://flight-data-server.azurewebsites.net/api/FlightData/filter',
      {
        headers: { pilotID: pilotID, flightCode: flightCode },
      }
    );
  }

  getPilotProfile(userID: string): Observable<any> {
    return this.httpClient.get('https://flight-data-server.azurewebsites.net/api/UsersAuth/getPilotProfile', {
      headers: {
        userID: userID,
      },
    });
  }
}
