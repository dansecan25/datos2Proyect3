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
    username:this.builder.control('',Validators.compose([Validators.required, Validators.minLength(5)])),
    password:this.builder.control('',Validators.compose([Validators.required, Validators.minLength(3)])),
    email:this.builder.control('',Validators.compose([Validators.required,Validators.email]))
  });
  extractFormData(): any {
    const { username, password, email } = this.registerForm.value;
    return { username, password, email };
  }
  proceedRegistration() {
    if (this.registerForm.valid) {
      const formData = this.extractFormData();
      this.service.ProceedRegister(formData).subscribe(res => {
        this.registerForm.reset(); // Reset the form after successful registration
        this.toastr.success('Successfully registered');
        this.router.navigate(['login']);
      });
    } else {
      this.toastr.warning('Data is not valid');
    }
  }
  
}
