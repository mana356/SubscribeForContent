import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Post } from 'src/app/models/Posts/post.model';
import { PostCreationDto } from 'src/app/models/Posts/post-creation.model';
import { SubscriptionLevelDto } from 'src/app/models/subscription-level.model';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(private http: HttpClient) {}

  GetUserHomeFeed(): Observable<Post[]> {
    return this.http.get<Post[]>(environment.apiURL + 'Posts');
  }
  GetCreatorPosts(username: string): Observable<Post[]> {
    let params = new HttpParams().set('CreatorUserName', username);
    return this.http.get<Post[]>(environment.apiURL + 'Posts', {
      params: params,
    });
  }

  GetCreatorSubscriptionLevels(
    username: string
  ): Observable<SubscriptionLevelDto[]> {
    return this.http.get<SubscriptionLevelDto[]>(
      environment.apiURL + 'SubscriptionLevels/' + username
    );
  }

  CreatePost(postData: PostCreationDto): Observable<any> {
    let headers = new HttpHeaders();
    headers.set('Content-Type', 'null');
    headers.set('Accept', 'multipart/form-data');
    let params = new HttpParams();

    var formData: any = new FormData();
    formData.append('Title', postData.Title);
    formData.append('Description', postData.Description);
    formData.append('Content', postData.Content);
    formData.append(
      'CreatorSubscriptionLevelId',
      postData.CreatorSubscriptionLevelId
    );
    if (
      postData.FileContents !== undefined &&
      postData.FileContents.length > 0
    ) {
      for (let i = 0; i < postData.FileContents.length; i++) {
        formData.append(
          'FileContents',
          postData.FileContents[i],
          postData.FileContents[i].name
        );
      }
    }

    return this.http.post<any>(environment.apiURL + 'Posts', formData, {
      params: params,
    });
  }

  DeletePost(id: number): Observable<any> {
    return this.http.delete<any>(environment.apiURL + 'Posts/' + id.toString());
  }
}
