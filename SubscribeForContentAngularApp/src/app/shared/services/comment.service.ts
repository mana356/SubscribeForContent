import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CommentDto } from 'src/app/models/Posts/comment.model';
import { CommentCreationDto } from 'src/app/models/Posts/comment-creation.model';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  constructor(private http: HttpClient) {}

  GetComments(postId: number): Observable<CommentDto[]> {
    let params = new HttpParams().set('PostId', postId);
    return this.http.get<CommentDto[]>(environment.apiURL + 'Comments', {
      params: params,
    });
  }

  CreateComment(commentData: CommentCreationDto): Observable<any> {
    return this.http.post<any>(environment.apiURL + 'Comments', commentData);
  }

  UpdateComment(id: number, body: string): Observable<any> {
    const payload = { body: body };
    return this.http.put<any>(
      environment.apiURL + 'Comments/' + id.toString(),
      payload
    );
  }

  UpdateLikeStatusComment(id: number, likeFlag: boolean): Observable<any> {
    const payload = { like: likeFlag };
    return this.http.put<any>(
      environment.apiURL + 'Comments/like/' + id.toString(),
      payload
    );
  }

  DeleteComment(id: number): Observable<any> {
    return this.http.delete<any>(
      environment.apiURL + 'Comments/' + id.toString()
    );
  }
}
