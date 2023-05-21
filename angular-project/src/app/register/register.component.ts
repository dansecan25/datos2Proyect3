import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import {ToastrService} from 'ngx-toastr'
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private builder:FormBuilder, private toastr:ToastrService, 
    private service:AuthService,
    private router:Router){

  }
  registerForm=this.builder.group({
    id:this.builder.control('',Validators.compose([Validators.required, Validators.minLength(3)])),
    username:this.builder.control('',Validators.required),
    password:this.builder.control('',Validators.required),
    email:this.builder.control('',Validators.compose([Validators.required,Validators.email]))
  });
  proceedRegistration(){
    if(this.registerForm.valid){
      this.service.ProceedRegister(this.registerForm.value).subscribe(res=>{
        this.toastr.success('Succesfuly registered');
        this.router.navigate(['login']);
      });
    }else{
      this.toastr.warning('Data is not valid');
    }
  }
}
