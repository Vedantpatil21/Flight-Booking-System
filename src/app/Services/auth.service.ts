import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl:string="https://localhost:7087/api/";
  private userPayload:any;

  constructor(private http:HttpClient, private router: Router) {
    this.userPayload=this.decodedToken();
  }
  signup(userObj:any){
    return this.http.post<any>(`${this.baseUrl}Authenticate/register`, userObj);
  }

  login(loginObj:any){
    return this.http.post<any>(`${this.baseUrl}Authenticate/login`, loginObj);
  }

  setToken(token:string){
    localStorage.setItem("token", token);
  }

  getToken(){
    return localStorage.getItem("token");
  }

  getAuthorizationHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.getToken()}`
    });
  }

  isLoggedIn():boolean{
    return (!!localStorage.getItem("token")); // 2 exclamation marks to convert string to boolean
  }

  signOut(){
    localStorage.clear();
    Swal.fire({
      title: 'Success!',
      text: "Logout Success!",
      icon: 'success',
      confirmButtonText: 'Ok'
    });
    this.router.navigate(['signin']);
  }

  search(searchObj:any){
    return this.http.post<any[]>(`${this.baseUrl}Search`, searchObj);
  }

  getJourneyById(id:number){
    return this.http.get(`${this.baseUrl}AirlineAuthority/${id}`);
  }

  
  bookTicket(bookObj:any){
    const headers = this.getAuthorizationHeaders();
    return this.http.post<any>(`${this.baseUrl}Booking`, bookObj, {headers});
  }

  addAirline(airlineObj:any){
    return this.http.post<any>(`${this.baseUrl}Authenticate/register_airline`, airlineObj);
  }

  getAllFlights(airlineName: any){
    return this.http.post<any[]>(`${this.baseUrl}AirlineAuthority/GetAllFlightOfAirline`, airlineName);
  }

  deleteFlight(id: number){
    return this.http.delete(`${this.baseUrl}AirlineAuthority/${id}`);
  }

  addFlight(flightObj:any){
    return this.http.post<any>(`${this.baseUrl}AirlineAuthority`, flightObj);
  }

  updateFlight(flightObj:any){
    return this.http.put(`${this.baseUrl}AirlineAuthority`, flightObj);
  }




  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken();
    return jwtHelper.decodeToken(token!);
  }

  getUserIdFromToken(){
    if(this.userPayload){
      return this.userPayload["jti"];
    }
  }

  getUsernameFromToken(){
    if(this.userPayload){
      return this.userPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    }
  }

  getEmailFromToken(){
    if(this.userPayload){
      return this.userPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
    }
  }

  getRoleFromToken(){
    if(this.userPayload){
      return this.userPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }
  }
}
