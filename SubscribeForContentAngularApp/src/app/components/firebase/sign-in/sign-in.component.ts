import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../shared/services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent implements OnInit {
  loginFormGroup: FormGroup;
  constructor(
    public authService: AuthService,
    public router: Router,
    private formBuilder: FormBuilder
  ) {
    this.loginFormGroup = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }
  ngOnInit() {
    if (this.authService.isLoggedIn) {
      this.router.navigate(['home']);
    }
  }
  errorHandling = (control: string, error: string) => {
    return this.loginFormGroup.controls[control].hasError(error);
  };

  validate() {
    if (this.loginFormGroup.invalid) {
      this.loginFormGroup.markAllAsTouched();
    }
    return this.loginFormGroup.valid;
  }

  googleLogin() {
    this.loginFormGroup.reset();
  }
}
