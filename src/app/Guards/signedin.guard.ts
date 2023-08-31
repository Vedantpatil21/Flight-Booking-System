import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { UserdataService } from '../Services/userdata.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard2 implements CanActivate {
  userRole:string="";
  constructor(private authService: AuthService, private router: Router, private userData:UserdataService, private auth:AuthService) {
    
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    this.userData.getRoleFromStore().subscribe(val=>{
      let userRoleFromToken=this.auth.getRoleFromToken();
      this.userRole=val || userRoleFromToken;
    });
    if (this.authService.isLoggedIn()) {
      if(this.userRole=="User"){
        this.router.navigate(['home']);
      }
      else if(this.userRole=="Airline"){
        this.router.navigate(['airline']);
      }
      else if(this.userRole=="Admin"){
        this.router.navigate(['adminhome']);
      }
      return false;
    } else {
      return true;
    }
  }

}

export const signedinGuard: CanActivateFn = (route, state) => {
  return true;
};
