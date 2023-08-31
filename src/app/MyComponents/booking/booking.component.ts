import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/Services/auth.service';
import { UserdataService } from 'src/app/Services/userdata.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent {
  userName: string="";
  age: string="";
  gender: string="";
  ticketType: string="";
  journeyId: number=0;
  userId:string="";

  row:any;

  private routeSubscription!: Subscription;
  constructor(private route:ActivatedRoute, private auth:AuthService, private router:Router, private userData:UserdataService){}

  ngOnInit() {
    // this.userData.getUserIdFromStore().subscribe(val=>{
    //   let userIdFromToken=this.auth.getUserIdFromToken();
    //   this.userId=val || userIdFromToken;
    // });
    // alert(this.userId);

    this.routeSubscription = this.route.queryParamMap.subscribe(params => {
      this.journeyId = Number(params.get('id'));
    });
      // alert(this.journeyId);
      this.auth.getJourneyById(this.journeyId).subscribe({
        next:(res)=>{
          this.row=res;
        },
        error:(err)=>{
          this.row=null;
          Swal.fire({
            title: 'Error!',
            text: err?.error.message,
            icon: 'error',
            confirmButtonText: 'Ok'
          });
        }
      });
  }

  onSubmit(){
    console.log(this.userName+" "+this.age+" "+this.gender+" "+this.ticketType+" "+this.journeyId);
    this.auth.bookTicket({
      userName: this.userName,
      age: this.age,
      gender: this.gender,
      ticketType: this.ticketType,
      journeyId: this.journeyId
    }).subscribe({
      next:(res)=>{
        Swal.fire({
          title: 'Success!',
          text: "Your Ticket is Booked Successfully! - SeatNo: "+res.seatNo+", Airline: "+res.airline+", Booking ID: "+res.bookingId,
          icon: 'success',
          confirmButtonText: 'Ok'
        });
        this.router.navigate(["home"]);
      },
      error:(err)=>{
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