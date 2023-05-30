import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Post } from 'src/app/models/Posts/post.model';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(private http: HttpClient) {}

  GetUserHomeFeed(): Observable<Post[]> {
    return this.http.get<Post[]>(environment.apiURL + 'Posts');
  }
}
