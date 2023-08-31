import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-adminhome',
  templateUrl: './adminhome.component.html',
  styleUrls: ['./adminhome.component.css']
})
export class AdminhomeComponent {
  name: string = "";
  email: string = "";
  password: string = "";
  cPassword: string = "";

  constructor(private auth:AuthService, private router:Router){}

  onSubmit() {
    let mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    let passwordFormat = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
    if (this.name.trim() == "" || this.email.trim() == "" || this.password.trim() == "" || this.cPassword.trim() == "") {
      // alert("Please Enter All The Details!");
      Swal.fire({
        title: 'Error!',
        text: "Please Enter All The Details!",
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      this.password="";
      this.cPassword="";
    }
    else if (!this.email.trim().match(mailformat)) {
      // alert("Please Enter a Valid Email ID!");
      Swal.fire({
        title: 'Error!',
        text: "Please Enter a Valid Email ID!",
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      this.password="";
      this.cPassword="";
    }
    else if (this.password.trim() != this.cPassword.trim()) {
      // alert("Password And Confirm Password Doesn't Match!");
      Swal.fire({
        title: 'Error!',
        text: "Password And Confirm Password Doesn't Match!",
        icon: 'error',
        confirmButtonText: 'Ok'
      });
      this.password="";
      this.cPassword="";
    }
    else if(!this.password.trim().match(passwordFormat)){
      // alert("Password Format:\nAt least one lowercase letter\nAt least one uppercase letter\nAt least one digit\nAt least one special character from the set @$!%*?&\nMinimum length of 8 characters");
      Swal.fire({
        title: 'Error!',
        text: "Password must be in a below format: At least one lowercase letter, At least one uppercase letter, At least one digit, At least one special character from the set @$!%*?&, Minimum length of 8 characters",
        icon: 'error',
        confirmButtonText: 'Ok'
      });
    }
    else {
      console.log(this.name + " " + this.email + " " + this.password + " " + this.cPassword);
      this.auth.addAirline({
        username: this.name.trim(),
        email: this.email.trim(),
        password: this.password.trim()
      }).subscribe({
        next:(res)=>{
          this.name="";
          this.email="";''
          this.password="";
          this.cPassword="";
          // alert(res.message);
          Swal.fire({
            title: 'Success!',
            text: "Airline Added Successfully!",
            icon: 'success',
            confirmButtonText: 'Ok'
          });
        },
        error:(err)=>{
          // alert(err?.error.message);
          Swal.fire({
            title: 'Error!',
            text: err?.error.message,
            icon: 'error',
            confirmButtonText: 'Ok'
          });
          this.password="";
          this.cPassword="";
        }
      });
    }
  }
}
