import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../shared/services/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
})
export class SignUpComponent implements OnInit {
  registerFormGroup: FormGroup;
  constructor(
    public authService: AuthService,
    private formBuilder: FormBuilder
  ) {
    this.registerFormGroup = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }
  ngOnInit() {}
  errorHandling = (control: string, error: string) => {
    return this.registerFormGroup.controls[control].hasError(error);
  };

  validate() {
    if (this.registerFormGroup.invalid) {
      this.registerFormGroup.markAllAsTouched();
    }
    return this.registerFormGroup.valid;
  }

  googleLogin() {
    this.registerFormGroup.reset();
  }
}
