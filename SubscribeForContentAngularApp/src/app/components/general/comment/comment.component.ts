import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommentDto } from 'src/app/models/Posts/comment.model';
import { CommentService } from 'src/app/shared/services/comment.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss'],
})
export class CommentComponent implements OnInit {
  @Input() commentData: CommentDto | undefined;
  isAuthorizedToDelete = false;
  isAuthorizedToEdit = false;
  constructor(
    private userService: UserService,
    private commentService: CommentService,
    private snackBar: MatSnackBar
  ) {}
  ngOnInit() {
    this.userService.CurrentUserProfile.subscribe((userData) => {
      if (userData?.userName === this.commentData?.user.userName) {
        this.isAuthorizedToDelete = true;
        this.isAuthorizedToEdit = true;
      }
    });
  }
  updateComment(updatedCommentBody: string) {
    if (
      this.commentData !== undefined &&
      updatedCommentBody !== null &&
      updatedCommentBody !== undefined &&
      updatedCommentBody !== ''
    ) {
      this.commentService
        .UpdateComment(this.commentData?.id, updatedCommentBody)
        .subscribe(() => {
          this.openSnackBar('Comment updated successfully!', 'success', 5);
          if (this.commentData !== undefined)
            this.commentData.body = updatedCommentBody;
        });
    }
  }
  editComment() {}
  deleteComment() {
    if (this.commentData !== undefined) {
      this.commentService.DeleteComment(this.commentData?.id).subscribe(() => {
        this.openSnackBar('Comment deleted successfully!', 'success', 5);
        this.commentData = undefined;
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
