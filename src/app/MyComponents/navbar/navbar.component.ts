import { Component } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  loggedIn:boolean=this.auth.isLoggedIn();
  constructor(private auth:AuthService){
    this.loggedIn=this.auth.isLoggedIn();
  }
  logout(){
    this.loggedIn=false;
    this.auth.signOut();
  }
}
