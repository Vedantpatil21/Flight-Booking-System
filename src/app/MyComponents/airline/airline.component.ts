import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { UserdataService } from 'src/app/Services/userdata.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-airline',
  templateUrl: './airline.component.html',
  styleUrls: ['./airline.component.css']
})
export class AirlineComponent {

  journeyIdN!:number;

  flightId!:string;
  airlineName!: string;
  startLoc!: string;
  endLoc!: string;
  startTime!: Date;
  endTime!: Date;
  eClassPrice!: number;
  bClassPrice!: number;
  eClassCapacity!: number;
  eClassAvailableSeats!: number;
  bClassCapacity!: number;
  bClassAvailableSeats!: number;

  flightData:any;

  currentDate:string = new Date().toISOString().slice(0,16);


  resultArray!:any;
  userName:string="";
  constructor(private auth: AuthService, private userData:UserdataService){}

  getAllFlights(userName:string){
    this.auth.getAllFlights({
      airlineName:userName
    }).subscribe({
      next:(res)=>{
        this.resultArray=res;
        if(res==null || res.length==0){
          Swal.fire({
            title: 'Error!',
            text: "No Any Flight To Show!",
            icon: 'error',
            confirmButtonText: 'Ok'
          });
        }
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

  ngOnInit(){
    this.userData.getUsernameFromStore().subscribe(val=>{
      let userNameFromToken=this.auth.getUsernameFromToken();
      this.userName=val || userNameFromToken;
      this.airlineName=val || userNameFromToken;
    });
    this.getAllFlights(this.userName);
  }

  deleteFlight(id:number){
    Swal.fire({
      title: "Do you want to delete?",
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: 'Yes',
      denyButtonText: 'No',
      customClass: {
        actions: 'my-actions',
        cancelButton: 'order-1 right-gap',
        confirmButton: 'order-2',
        denyButton: 'order-3',
      }
    }).then((result) => {
      if (result.isConfirmed) {
        this.auth.deleteFlight(id).subscribe({
          next:(res)=>{
            Swal.fire('Deleted!', '', 'success')
            this.getAllFlights(this.userName);
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
      } else if (result.isDenied) {
        Swal.fire('Deletetion Cancelled!', '', 'info');
      }
    });  
  }

  editFlightClicked(id:number){
    this.auth.getJourneyById(id).subscribe({
      next:(res)=>{
        this.flightData=res;
        this.journeyIdN=this.flightData.journeyId;
        this.flightId = this.flightData.flightId;
        this.airlineName=this.flightData.airlineName;
        this.startLoc =this.flightData.startLoc;
        this.endLoc =this.flightData.endLoc;
        this.startTime =this.flightData.startTime;
        this.endTime =this.flightData.endTime;
        this.eClassPrice =this.flightData.eClassPrice;
        this.bClassPrice =this.flightData.bClassPrice;
        this.eClassCapacity =this.flightData.eClassCapacity;
        this.eClassAvailableSeats =this.flightData.eClassAvailableSeats;
        this.bClassCapacity =this.flightData.bClassCapacity;
        this.bClassAvailableSeats =this.flightData.bClassAvailableSeats;
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

  editFlight(){
    if(this.flightId == null ||
      this.airlineName==null ||
      this.startLoc == null ||
      this.endLoc == null || 
      this.startTime == null || 
      this.endTime == null || 
      this.eClassPrice == null || 
      this.bClassPrice == null || 
      this.eClassCapacity == null || 
      this.eClassAvailableSeats == null || 
      this.bClassCapacity == null || 
      this.bClassAvailableSeats  == null ||
      this.flightId=="" ||
      this.startLoc=="" ||
      this.endLoc==""){
        Swal.fire("Error!", "Please enter all details", "error");
      }
      else{
        this.auth.updateFlight({
          journeyId:this.journeyIdN,
          flightId:this.flightId,
          airlineName:this.userName,
          startLoc:this.startLoc,
          endLoc:this.endLoc,
          startTime:this.startTime,
          endTime:this.endTime,
          eClassPrice:this.eClassPrice,
          bClassPrice:this.bClassPrice,
          eClassCapacity:this.eClassCapacity,
          eClassAvailableSeats:this.eClassAvailableSeats,
          bClassCapacity:this.bClassCapacity, 
          bClassAvailableSeats:this.bClassAvailableSeats
        }).subscribe({
          next:(res)=>{
            Swal.fire("Success!", "Fligth Details Updated Successfully!", "success");
            document.getElementById("updateFlightModalClose")?.click();
            this.flightId = "";
            // this.airlineName == null;
            this.startLoc = "";
            this.endLoc = "";
            this.startTime = new Date();
            this.endTime = new Date();
            this.eClassPrice = 0;
            this.bClassPrice = 0;
            this.eClassCapacity = 0;
            this.eClassAvailableSeats = 0;
            this.bClassCapacity = 0;
            this.bClassAvailableSeats = 0;
            this.getAllFlights(this.userName);
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

  addFlight(){
    if(this.flightId == null ||
      this.airlineName==null ||
      this.startLoc == null ||
      this.endLoc == null || 
      this.startTime == null || 
      this.endTime == null || 
      this.eClassPrice == null || 
      this.bClassPrice == null || 
      this.eClassCapacity == null || 
      this.eClassAvailableSeats == null || 
      this.bClassCapacity == null || 
      this.bClassAvailableSeats  == null ||
      this.flightId=="" ||
      this.startLoc=="" ||
      this.endLoc==""){
        Swal.fire("Error!", "Please enter all details", "error");
      }
      else{
        this.auth.addFlight({
          flightId:this.flightId,
          airlineName:this.userName,
          startLoc:this.startLoc,
          endLoc:this.endLoc,
          startTime:this.startTime,
          endTime:this.endTime,
          eClassPrice:this.eClassPrice,
          bClassPrice:this.bClassPrice,
          eClassCapacity:this.eClassCapacity,
          eClassAvailableSeats:this.eClassAvailableSeats,
          bClassCapacity:this.bClassCapacity, 
          bClassAvailableSeats:this.bClassAvailableSeats
        }).subscribe({
          next:(res)=>{
            Swal.fire("Success!", "Fligth Details Added Successfully!", "success");
            document.getElementById("addFlightModalClose")?.click();
            this.flightId = "";
            // this.airlineName == null;
            this.startLoc = "";
            this.endLoc = "";
            this.startTime = new Date();
            this.endTime = new Date();
            this.eClassPrice = 0;
            this.bClassPrice = 0;
            this.eClassCapacity = 0;
            this.eClassAvailableSeats = 0;
            this.bClassCapacity = 0;
            this.bClassAvailableSeats = 0;
            this.getAllFlights(this.userName);
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
}
