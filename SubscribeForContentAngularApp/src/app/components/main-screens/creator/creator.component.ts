import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { UserProfile } from 'src/app/models/UserProfile/user-profile.model';
import { Post } from 'src/app/models/Posts/post.model';
import { PostService } from 'src/app/shared/services/post.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-creator',
  templateUrl: './creator.component.html',
  styleUrls: ['./creator.component.scss'],
})
export class CreatorComponent {
  constructor(
    private userService: UserService,
    private postService: PostService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  userName = '';
  creatorPosts: Post[] = [];
  creator: UserProfile | undefined;
  coverPictureUrl = '';
  profilePictureUrl = '';
  totalPosts = 0;
  myProfileFlag = false;

  ngOnInit() {
    this.userName = this.route.snapshot.paramMap.get('username') ?? '';
    if (
      this.userName === '' ||
      this.userName === null ||
      this.userName === undefined
    ) {
      this.router.navigate(['sign-in']);
    }
    this.userService.CurrentUserProfile.subscribe((userData) => {
      if (userData != undefined) {
        this.myProfileFlag =
          userData.userName.toLowerCase() === this.userName.toLocaleLowerCase();
      }
    });
    this.getCreatorDetails();
    this.getPostsData();
  }

  getCreatorDetails() {
    this.userService.GetUserDetails(this.userName).subscribe((result) => {
      this.creator = result;
      if (this.creator.profilePicture) {
        this.profilePictureUrl = this.creator.profilePicture.url;
      } else {
        this.profilePictureUrl = '/assets/dummy-user.png';
      }
      if (this.creator.coverPicture) {
        this.profilePictureUrl = this.creator.coverPicture.url;
      } else {
        this.coverPictureUrl = '/assets/solid-black.png';
      }
    });
  }

  getPostsData() {
    this.postService.GetCreatorPosts(this.userName).subscribe((result) => {
      this.creatorPosts = result.sort((a, b) => {
        if (a.createdOn > b.createdOn) {
          return -1;
        } else return 1;
      });
      this.totalPosts = result.length;
    });
  }
}
