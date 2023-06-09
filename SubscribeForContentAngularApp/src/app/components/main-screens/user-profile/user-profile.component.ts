import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserProfileUpdateDto } from 'src/app/models/UserProfile/user-profile-update.model';
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
    private userService: UserService,
    private snackBar: MatSnackBar
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
      dateOfBirth: [new Date(), [Validators.required]],
      bio: ['', [Validators.maxLength(500)]],
      isACreator: [false],
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
      this.accountSettingsFormGroup.controls['isACreator'].setValue(
        user?.isACreator
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

  openSnackBar(message: string, type?: string, durationInSeconds?: number) {
    let panelClass = 'default-snackbar';
    if (type) {
      if (type === 'success') {
        panelClass = 'success-snackbar';
      } else {
        panelClass = 'failure-snackbar';
      }
    }
    if (durationInSeconds) {
      this.snackBar.open(message, 'OK', {
        duration: 1000 * durationInSeconds,
        panelClass: [panelClass, 'login-snackbar'],
      });
    } else {
      this.snackBar.open(message, 'OK', {
        panelClass: [panelClass, 'login-snackbar'],
      });
    }
  }

  onSubmit(): void {
    if (!this.validate()) {
      return;
    }
    const profileData: UserProfileUpdateDto = {
      Name: this.accountSettingsFormGroup.controls['name'].value,
      UserName: this.accountSettingsFormGroup.controls['username'].value,
      Bio: this.accountSettingsFormGroup.controls['bio'].value,
      DateOfBirth:
        this.accountSettingsFormGroup.controls[
          'dateOfBirth'
        ].value.toDateString(),
      IsACreator: this.accountSettingsFormGroup.controls['isACreator'].value,
    };

    this.userService.UpdateUserProfile(profileData).subscribe(
      (res) => {
        this.openSnackBar('Settings saved successfully!', 'success', 10);
      },
      (error) => {
        this.openSnackBar(
          'Some error occurred! Please see following details: \n' +
            error.error.errorMessage
        );
      }
    );
  }
}
