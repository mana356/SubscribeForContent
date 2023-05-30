import { Component } from '@angular/core';
import { Creator } from 'src/app/models/creator.model';
import { Post } from 'src/app/models/Posts/post.model';

@Component({
  selector: 'app-creator',
  templateUrl: './creator.component.html',
  styleUrls: ['./creator.component.scss'],
})
export class CreatorComponent {
  creatorPosts: Post[] = [];
  creator: Creator = {
    Id: 1,
    UserName: 'anamics93',
    JoinedOn: new Date('2022-10-22'),
    TotalPosts: 3,
    BiographyText: 'Creating memes for fun since 2019!',
    ProfilePictureUrl:
      'https://images.pexels.com/photos/1674752/pexels-photo-1674752.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1',
    CoverPictureUrl:
      'https://images.unsplash.com/photo-1623627484632-f041d1fb366d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8Y292ZXIlMjBwaG90b3xlbnwwfHwwfHw%3D&w=1000&q=80',
  };

  ngOnInit() {}
}
