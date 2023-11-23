import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingpageComponent } from './landingpage/landingpage.component';
import { InfopageModule } from './infopage/infopage.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ChartComponent } from './chart/chart.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InfoDashboardComponent } from './info-dashboard/info-dashboard.component';
import { MapComponent } from './map/map.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RegisterComponent } from './register/register.component';
import { UsercontextService } from './usercontext.service';
import { HowtouseComponent } from './howtouse/howtouse.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    ChartComponent,
    InfoDashboardComponent,
    MapComponent,
    NavbarComponent,
    LandingpageComponent,
    RegisterComponent,
    HowtouseComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    InfopageModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
  ],
  providers: [UsercontextService],
  bootstrap: [AppComponent],
})
export class AppModule {}
