import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Route } from '@angular/router';
import { CommentDto } from 'src/app/models/Posts/comment.model';
import { Post } from 'src/app/models/Posts/post.model';
import { CommentService } from 'src/app/shared/services/comment.service';
import { PostService } from 'src/app/shared/services/post.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss'],
})
export class PostDetailComponent implements OnInit {
  postId: number | undefined;
  postData: Post | undefined;
  comments: CommentDto[] | undefined;
  constructor(
    private userService: UserService,
    private postService: PostService,
    private commentService: CommentService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.postId = Number(this.route.snapshot.paramMap.get('id') ?? 0);
    this.postService.GetPostById(this.postId).subscribe((res) => {
      this.postData = res;
    });
    this.commentService.GetComments(this.postId).subscribe((res) => {
      this.comments = res;
    });
  }
}
