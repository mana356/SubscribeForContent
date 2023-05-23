import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../shared/services/auth.service';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent implements OnInit {
  resetFormGroup: FormGroup;

  constructor(
    public authService: AuthService,
    private formBuilder: FormBuilder
  ) {
    this.resetFormGroup = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  ngOnInit() {}

  errorHandling = (control: string, error: string) => {
    return this.resetFormGroup.controls[control].hasError(error);
  };

  validate() {
    if (this.resetFormGroup.invalid) {
      this.resetFormGroup.markAllAsTouched();
    }
    return this.resetFormGroup.valid;
  }
}
