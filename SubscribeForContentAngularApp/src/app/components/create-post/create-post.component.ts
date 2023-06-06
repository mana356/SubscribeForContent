import { Component, HostListener, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { PostCreationDto } from 'src/app/models/Posts/post-creation.model';
import { SubscriptionLevelDto } from 'src/app/models/subscription-level.model';
import { PostService } from 'src/app/shared/services/post.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.scss'],
})
export class CreatePostComponent implements OnInit {
  form: FormGroup = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    content: new FormControl(''),
    subscriptionLevel: new FormControl(''),
  });
  submitted = false;
  files: any[] = [];
  subscriptionLevels: SubscriptionLevelDto[] = [];
  username: string = '';
  constructor(
    private formBuilder: FormBuilder,
    private postService: PostService,
    private userService: UserService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(100),
        ],
      ],
      description: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(100),
        ],
      ],
      content: ['', [Validators.maxLength(1000)]],
      subscriptionLevel: ['', [Validators.required]],
    });
    this.userService.CurrentUserProfile.subscribe((userData) => {
      if (userData && userData.userName != undefined) {
        this.username = userData?.userName;

        this.postService
          .GetCreatorSubscriptionLevels(userData?.userName)
          .subscribe((res) => {
            this.subscriptionLevels = res;
          });
      }
    });
  }

  /* Handle form errors in Angular 8 */
  public errorHandling = (control: string, error: string) => {
    return this.form.controls[control].hasError(error);
  };

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
    debugger;
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    const postData: PostCreationDto = {
      Title: this.form.controls['title'].value,
      Description: this.form.controls['description'].value,
      Content: this.form.controls['content'].value,
      CreatorSubscriptionLevelId: this.form.controls['subscriptionLevel'].value,
      FileContents: this.files,
    };

    this.postService.CreatePost(postData).subscribe(
      (res) => {
        this.openSnackBar('Posted successfully!', 'success', 10);
        this.router.navigate(['creator', this.username]);
      },
      (error) => {
        this.openSnackBar(
          'Some error occurred! Please see following details: \n' +
            error.error.errorMessage
        );
      }
    );
  }

  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
  /**
   * on file drop handler
   */
  onFileDropped($event: any[]) {
    this.prepareFilesList($event);
  }

  /**
   * handle file from browsing
   */
  fileBrowseHandler(e?: any) {
    let files: any = e.target.files;
    this.prepareFilesList(files);
  }

  /**
   * Delete file from files list
   * @param index (File index)
   */
  deleteFile(index: number) {
    this.files.splice(index, 1);
  }

  /**
   * Simulate the upload process
   */
  uploadFilesSimulator(index: number) {
    setTimeout(() => {
      if (index === this.files.length) {
        return;
      } else {
        const progressInterval = setInterval(() => {
          if (this.files[index].progress === 100) {
            clearInterval(progressInterval);
            this.uploadFilesSimulator(index + 1);
          } else {
            this.files[index].progress += 5;
          }
        }, 200);
      }
    }, 1000);
  }

  /**
   * Convert Files list to normal array list
   * @param files (Files List)
   */
  prepareFilesList(files: Array<any>) {
    for (const item of files) {
      item.progress = 0;
      const reader = new FileReader();
      reader.onload = (e: any) => {
        item.imageSrc = e.target.result;
      };
      reader.readAsDataURL(item);
      this.files.push(item);
    }
    this.uploadFilesSimulator(0);
  }

  /**
   * format bytes
   * @param bytes (File size in bytes)
   * @param decimals (Decimals point)
   */
  formatBytes(bytes: number, decimals: number) {
    if (bytes === 0) {
      return '0 Bytes';
    }
    const k = 1024;
    const dm = decimals <= 0 ? 0 : decimals || 2;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
  }
}

interface HTMLInputEvent extends Event {
  target: HTMLInputElement & EventTarget;
}
