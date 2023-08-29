import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  startLoc: string = "";
  endLoc: string = "";
  startDate: string = "";
  currentDate: string = new Date().toISOString().slice(0, 10);
  resultArray!:any[];
  responseStatusCode!:number;

  constructor(private auth:AuthService, private router:Router){}

  // ngAfterViewInit():void{
  //   if(localStorage.getItem("startDate")!=null){
  //     this.startDate=localStorage.getItem("startDate")!;
  //   }

  //   if(localStorage.getItem("startLoc")!=null){
  //     this.startDate=localStorage.getItem("startLoc")!;
  //   }

  //   if(localStorage.getItem("endLoc")!=null){
  //     this.startDate=localStorage.getItem("endLoc")!;
  //   }
  // }

  onSubmit() {
    // if(this.startDate.trim()!=""){
    //   localStorage.setItem("startDate", this.startDate);
    // }
    // if(this.startLoc.trim()!=""){
    //   localStorage.setItem("startLoc", this.startLoc);
    // }
    // if(this.endLoc.trim()!=""){
    //   localStorage.setItem("endLoc", this.endLoc);
    // }

    if (this.startLoc.trim() == "" || this.endLoc.trim() == "" || this.startDate.trim() == "") {
      // alert("Please Enter All The Details!");
      Swal.fire({
        title: 'Error!',
        text: 'Please Enter All The Details!',
        icon: 'error',
        confirmButtonText: 'Ok'
      });
    }
    else {
      console.log(this.startLoc + " " + this.endLoc + " " + this.startDate);
      this.auth.search({
        startDate: this.startDate,
        startLoc: this.startLoc,
        endLoc: this.endLoc
      }).subscribe({
        next:(res)=>{
          // alert(res.message);
          // this.router.navigate(['signin']);
          this.resultArray=res;
          // this.responseStatusCode=res.status;
          // alert(res);
        },
        error:(err)=>{
          this.resultArray=[];
          // alert(err?.error.message);
          Swal.fire({
            title: 'Error!',
            text: err?.error.message,
            icon: 'error',
            confirmButtonText: 'Ok'
          });
        }
      });
    }
  }
  bookBtn(jId:number){
    // alert(jId);
    this.router.navigate(['booking'], {queryParams:{id:jId}});
  }
}
