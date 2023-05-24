import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {ToastrService} from 'ngx-toastr'
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userData:any;

  constructor(private builder:FormBuilder, private toastr:ToastrService, 
    private service:AuthService, private router:Router){
      sessionStorage.clear();
  }
  loginForm=this.builder.group({
    username:this.builder.control('',Validators.required),
    password:this.builder.control('',Validators.required),
   });
   proceedLogin() {
    if (this.loginForm.valid) {
      const username = this.loginForm.value.username ?? '';
      this.service.getByUsername(username).subscribe(res => {
        this.userData = res;
        if (this.userData && this.userData.password === this.loginForm.value.password) {
          sessionStorage.setItem('username', this.userData.username);
          this.router.navigate(['']);
        } else if (!this.userData) {
          this.toastr.warning('User not found');
        } else {
          this.toastr.warning('Password not valid');
        }
      });
    } else {
      this.toastr.warning('Please enter valid data.');
    }
  }
  
}
