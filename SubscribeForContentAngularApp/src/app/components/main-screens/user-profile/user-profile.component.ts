import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserProfile } from 'src/app/models/UserProfile/user-profile.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent {
  accountSettingsFormGroup: FormGroup;
  minDate = new Date(1900, 0, 1);
  maxDate = new Date(new Date().getFullYear() - 8, 12, 0);
  currentUser: UserProfile | undefined;
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService
  ) {
    this.accountSettingsFormGroup = this.formBuilder.group({
      username: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(30),
        ],
      ],
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(255),
        ],
      ],
      dateOfBirth: [''],
      bio: ['', [Validators.maxLength(500)]],
    });
  }

  ngOnInit() {
    this.userService.CurrentUserProfile.subscribe((user) => {
      this.currentUser = user;
      this.accountSettingsFormGroup.controls['username'].setValue(
        user?.userName
      );
      this.accountSettingsFormGroup.controls['name'].setValue(user?.name);
      this.accountSettingsFormGroup.controls['bio'].setValue(user?.bio);
      this.accountSettingsFormGroup.controls['dateOfBirth'].setValue(
        user?.dateOfBirth
      );
    });
  }

  errorHandling = (control: string, error: string) => {
    return this.accountSettingsFormGroup.controls[control].hasError(error);
  };

  validate() {
    if (this.accountSettingsFormGroup.invalid) {
      this.accountSettingsFormGroup.markAllAsTouched();
    }
    return this.accountSettingsFormGroup.valid;
  }
}
