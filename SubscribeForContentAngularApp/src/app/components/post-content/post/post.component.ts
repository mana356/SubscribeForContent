import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Post } from 'src/app/models/Posts/post.model';
import { PostService } from 'src/app/shared/services/post.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent implements OnInit {
  @Input() post: Post | undefined;
  @Input() detailMode: boolean = false;
  constructor(
    private userService: UserService,
    private postService: PostService,
    private snackBar: MatSnackBar
  ) {}
  isAuthorizedToDelete = false;
  ngOnInit() {
    this.userService.CurrentUserProfile.subscribe((userData) => {
      if (userData?.userName === this.post?.creator.userName) {
        this.isAuthorizedToDelete = true;
      }
    });
  }
  deletePost() {
    if (this.post !== undefined) {
      this.postService.DeletePost(this.post?.id).subscribe(() => {
        this.openSnackBar('Post deleted successfully!', 'success', 5);
        this.post = undefined;
      });
    }
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
}
