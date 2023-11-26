import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { Apiservice } from './api.service';

describe('ApiserviceService', () => {
  let service: Apiservice;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpClient, HttpHandler],
    });
    service = TestBed.inject(Apiservice);


  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

});
