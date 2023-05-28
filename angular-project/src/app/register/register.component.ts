import { Component,HostListener} from '@angular/core';
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

  constructor(
    private builder: FormBuilder,
    private toastr: ToastrService,
    private service: AuthService,
    private router: Router
  ) {}
  currentPassword: string = '';
  arduinoPasswordValidator: ValidatorFn = (control: AbstractControl) => {
    const value = control.value ? control.value.toString() : '';
  
    if (value && !/^[01]*$/.test(value)) {
      return { invalidPassword: true };
    }
    return null;
  };
  @HostListener('window:keydown', ['$event'])
    onKeyDown(event: KeyboardEvent) {
      const key = event.key;
      this.handleSerialData(key);
    }
  registerForm = this.builder.group({
    username: ['', [Validators.required, Validators.minLength(5)]],
    password: ['', [Validators.required, Validators.minLength(3), this.arduinoPasswordValidator]],
    email: ['', [Validators.required, Validators.email]]
  });
  handleSerialData(data: string) {
    if (data === '0' || data === '1') { //handler for the input from the serial port
      this.currentPassword += data;
      this.registerForm.get('password')?.setValue(this.currentPassword);
    } else if (data === 'Backspace') {
      this.currentPassword = this.currentPassword.slice(0, -1);
      this.registerForm.get('password')?.setValue(this.currentPassword);
    }
  }
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
