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
    private service:AuthService,
    private router:Router){

  }
  loginForm=this.builder.group({
    username:this.builder.control('',Validators.required),
    password:this.builder.control('',Validators.required),
   });
   proceedLogin(){
    if(this.loginForm.valid){
    }
    this.service.GetByCode(this.loginForm.value.username).subscribe(res=>{
      this.userData=res;
      console.log(this.userData);
      if(this.userData.password===this.loginForm.value.password){
        sessionStorage.setItem('username',this.userData.username);
        this.router.navigate(['']);
      }else{
        this.toastr.warning('Not valid');
      }
    });
  }
}
