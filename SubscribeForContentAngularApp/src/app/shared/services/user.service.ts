import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  CreateUser(): Observable<any> {
    return this.http.post(
      environment.apiURL + 'Users/CreateUserIfDoesNotExist',
      null
    );
  }
  GetCurrentUserDetails(): Observable<any> {
    return this.http.get(
      environment.apiURL + 'Users/' + this.authService.userData.uid
    );
  }
}
