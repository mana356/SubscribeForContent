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
    const post1: Post = {
      Id: 1,
      Title: 'First Post!',
      Description: 'This is my first post',
      ImageUrl:
        'https://www.news-medical.net/image.axd?picture=2019%2F4%2FBy_Rido.jpg',
      Content:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam luctus eleifend cursus. Curabitur nisl sem, euismod porta lacus quis, malesuada tincidunt metus. Donec nec nunc fermentum orci pulvinar feugiat. Proin feugiat dolor non consequat commodo. Suspendisse consectetur eros metus, in semper lorem volutpat quis. Mauris pulvinar viverra mauris quis rutrum. Ut maximus semper tortor, quis dapibus leo imperdiet in.',
      CreatorId: 1,
      CreatorName: 'anamics93',
      CreatedDate: new Date(),
      IsLiked: true,
    };
    this.homePosts.push(post1);

    const post2: Post = {
      Id: 1,
      Title: 'Second Post!',
      Description: 'This is my second post',
      Content:
        'Morbi mattis varius sodales. Morbi vitae quam eget lectus ullamcorper ultrices. Sed finibus sodales metus, et eleifend tellus tincidunt id. Donec vitae nibh ac sapien tincidunt finibus. Maecenas felis tortor, posuere non condimentum eu, pharetra eu urna. Maecenas sed sodales tortor, quis pulvinar metus. Nullam tempor sapien ac risus aliquam scelerisque. Phasellus iaculis elementum eros non pharetra. Nullam a ipsum felis. Suspendisse potenti. Integer nec nibh felis. Pellentesque elementum mollis augue, vitae euismod felis vehicula sit amet. Quisque nec mauris mauris. Nulla aliquam lorem sed augue porta interdum. Suspendisse euismod egestas sapien, non dictum sem porta id. Vivamus ut leo ultrices, pharetra arcu vel, ullamcorper nunc. \nDonec vitae libero in ipsum imperdiet mattis a sed felis. Proin velit felis, aliquet id molestie vel, pharetra ac purus. Aliquam accumsan elit vitae feugiat auctor. Proin eu nunc vel mi iaculis venenatis. Quisque urna ante, pretium quis feugiat non, finibus in est. Duis vel pharetra odio, eu venenatis nisi. Nullam dignissim odio at felis congue tempor. Nam elit leo, hendrerit quis facilisis sed, hendrerit at nisi. Aliquam sit amet turpis leo. Curabitur dictum imperdiet libero quis tristique.',
      CreatorId: 1,
      CreatorName: 'anamics93',
      CreatedDate: new Date(),
      IsLiked: true,
    };
    this.homePosts.push(post2);

    const post3: Post = {
      Id: 1,
      Title: 'Third Post!',
      Description: 'This is my third post',
      Content:
        'Donec vitae libero in ipsum imperdiet mattis a sed felis. Proin velit felis, aliquet id molestie vel, pharetra ac purus. Aliquam accumsan elit vitae feugiat auctor. Proin eu nunc vel mi iaculis venenatis. Quisque urna ante, pretium quis feugiat non, finibus in est. Duis vel pharetra odio, eu venenatis nisi. Nullam dignissim odio at felis congue tempor. Nam elit leo, hendrerit quis facilisis sed, hendrerit at nisi. Aliquam sit amet turpis leo. Curabitur dictum imperdiet libero quis tristique.\nSuspendisse placerat venenatis commodo. Vivamus accumsan est ac erat interdum egestas. In libero lacus, bibendum ultrices hendrerit non, pellentesque nec lacus. Donec in diam vitae lacus cursus dapibus. Pellentesque varius, eros a tincidunt dignissim, nunc ligula rutrum lorem, ornare scelerisque ex felis in turpis. Nam ut ultricies odio. Nunc tincidunt libero eget ante aliquet condimentum. Nam lacinia odio arcu, porttitor efficitur lorem feugiat sit amet. Vivamus hendrerit velit in mollis sodales. Donec sit amet ultrices mauris. Morbi augue erat, molestie et dignissim eget, finibus eget turpis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec id sagittis enim. Nulla dapibus a mi sit amet pretium. Vivamus venenatis sapien eget aliquet faucibus.\nNam maximus risus eu lectus lobortis, id facilisis diam bibendum. Cras gravida, lacus non lobortis pulvinar, sem sapien egestas justo, ac molestie dui dui vel lectus. Nunc rhoncus sed tortor et tristique. Praesent dignissim, massa sed placerat ullamcorper, risus nibh aliquet eros, scelerisque scelerisque felis tellus eget lectus. Mauris eu dapibus massa. Proin ornare erat odio, eget tincidunt dui ornare quis. Proin diam libero, sodales ac iaculis a, tempus at ex. Donec feugiat maximus tellus, ac tristique dui gravida at.',
      CreatorId: 1,
      CreatorName: 'anamics93',
      CreatedDate: new Date(),
      IsLiked: false,
    };
    this.homePosts.push(post3);
  }

  getData() {
    this.postService.GetUserHomeFeed().subscribe((result) => {
      console.log(result);
    });
  }
}
