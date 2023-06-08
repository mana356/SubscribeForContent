import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/Posts/post.model';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.scss'],
})
export class UserHomeComponent implements OnInit {
  homePosts: Post[] = [];
  constructor(private postService: PostService) {}

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.postService.GetUserHomeFeed().subscribe((result) => {
      this.homePosts = result.sort((a, b) => {
        if (a.createdOn > b.createdOn) {
          return -1;
        } else return 1;
      });
    });
  }
}
