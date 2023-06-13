import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Post } from 'src/app/models/Posts/post.model';
import { PostService } from 'src/app/shared/services/post.service';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.scss'],
})
export class UserHomeComponent implements OnInit {
  private PostList$ = new BehaviorSubject<Post[] | undefined>(undefined);
  public postList = this.PostList$.asObservable();

  constructor(private postService: PostService) {}

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.postService.GetUserHomeFeed().subscribe((result) => {
      const sortedPosts = result.sort((a, b) => {
        if (a.createdOn > b.createdOn) {
          return -1;
        } else return 1;
      });
      this.PostList$.next(sortedPosts);
      console.log(sortedPosts);
    });
  }
}
