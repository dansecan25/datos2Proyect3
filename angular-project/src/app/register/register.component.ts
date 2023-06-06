import { Component} from '@angular/core';
import { FormBuilder, Validators, ValidatorFn, FormControl, AbstractControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  port: any = null;
  constructor(
    private builder: FormBuilder,
    private toastr: ToastrService,
    private service: AuthService,
    private router: Router,
  ) {}
  //requestPort=async()=>{
   // try{
    //  const serialPort = await (navigator as any).serial.requestPort();
    //  await serialPort.open({baudRate:9600});
    //  this.port = serialPort;
    //}catch (error){
    //  console.log(error);
    //}
  //};
  currentPassword: string = '';
  passwordValidator: ValidatorFn = (control: AbstractControl) => {
    const value = control.value ? control.value.toString() : '';

    if (value && !/^[01]+$/.test(value)) {
      return { invalidPassword: true };
    }
    return null;
  };
  
  registerForm = this.builder.group({
    username: ['', [Validators.required, Validators.minLength(5)]],
    password: ['', [Validators.required, Validators.minLength(3), this.passwordValidator]],
    email: ['', [Validators.required, Validators.email]]
  });
  extractFormData(): any {
    const { username, password, email } = this.registerForm.value;
    return { username, password, email };
  }

  proceedRegistration() {
    if (this.registerForm.valid) {
      this.service.getLastId().subscribe(lastId => {
        const formData = this.extractFormData();
        formData.id = lastId + 1;
        this.service.ProceedRegister(formData).subscribe(res => {
          this.registerForm.reset();
          this.toastr.success('Successfully registered');
          this.router.navigate(['login']);
        });
      });
    } else {
      this.toastr.warning('Data is not valid');
    }
  }
}
