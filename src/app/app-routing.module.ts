 import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookingComponent } from './MyComponents/booking/booking.component';
import { HomeComponent } from './MyComponents/home/home.component';
import { SigninComponent } from './MyComponents/signin/signin.component';
import { SignupComponent } from './MyComponents/signup/signup.component';
import { AuthGuard1 } from './Guards/auth.guard';
import { AuthGuard2 } from './Guards/signedin.guard';
import { AdminhomeComponent } from './MyComponents/adminhome/adminhome.component';
import { AirlineComponent } from './MyComponents/airline/airline.component';
import { AuthGuardAdmin } from './Guards/adminguard.guard';
import { AuthGuardAirline } from './Guards/airlineguard.guard';


const routes: Routes = [
  { path: '', redirectTo:'home', pathMatch:'full' },
  { path: 'home', component: HomeComponent },
  { path: 'signup', component: SignupComponent, canActivate:[AuthGuard2]},
  { path: 'signin', component: SigninComponent, canActivate:[AuthGuard2] },
  { path: 'booking', component: BookingComponent, canActivate:[AuthGuard1] },
  { path: 'adminhome', component: AdminhomeComponent, canActivate:[AuthGuardAdmin] },
  { path: 'airline', component: AirlineComponent, canActivate:[AuthGuardAirline] }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
