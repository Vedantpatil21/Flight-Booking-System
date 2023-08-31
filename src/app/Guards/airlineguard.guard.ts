import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { UserdataService } from '../Services/userdata.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardAirline implements CanActivate {
  userRole:string="";
  constructor(private authService: AuthService, private router: Router, private userData: UserdataService, private auth:AuthService) {
    
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    this.userData.getRoleFromStore().subscribe(val=>{
      let userRoleFromToken=this.auth.getRoleFromToken();
      this.userRole=val || userRoleFromToken;
    });
    if (this.authService.isLoggedIn() && this.userRole=="Airline") {
      return true;
    } else {
      this.router.navigate(['signin']);
      return false;
    }
  }
}

export const authGuard: CanActivateFn = (route, state) => {
  return true;
};