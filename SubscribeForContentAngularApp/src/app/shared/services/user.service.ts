import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  CreateUserIfDoesNotExist(): Observable<any> {
    return this.http.post(
      environment.apiURL + 'Users/CreateUserIfDoesNotExist',
      null
    );
  }
  GetUserDetails(userId: string): Observable<any> {
    return this.http.get(environment.apiURL + 'Users/' + userId);
  }
}
