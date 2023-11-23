import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InfopageComponent } from './infopage/infopage.component';
import { LandingpageComponent } from './landingpage/landingpage.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ChartComponent } from './chart/chart.component';
import { InfoDashboardComponent } from './info-dashboard/info-dashboard.component';
import { RegisterComponent } from './register/register.component';
import { HowtouseComponent } from './howtouse/howtouse.component';

const routes: Routes = [
  {
    component: LandingpageComponent,
    path: '',
  },
  {
    component: LoginComponent,
    path: 'login',
  },
  {
    component: RegisterComponent,
    path: 'register',
  },
  {
    component: InfopageComponent,
    path: 'info',
  },
  {
    component: HowtouseComponent,
    path: 'howtouse',
  },
  {
    component: DashboardComponent,
    path: 'dashboard',
    children: [
      {
        path: 'info',
        component: InfoDashboardComponent,
      },
    ],
  },
  {
    component: ChartComponent,
    path: 'chart',
  },
  { path: 'login', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', component: LandingpageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
