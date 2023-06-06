import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserProfile } from 'src/app/models/UserProfile/user-profile.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private CurrentUser$ = new BehaviorSubject<UserProfile | undefined>(
    undefined
  );
  public CurrentUserProfile = this.CurrentUser$.asObservable();

  constructor(private http: HttpClient) {}

  CreateUserIfDoesNotExist(): Observable<any> {
    return this.http.post(
      environment.apiURL + 'Users/CreateUserIfDoesNotExist',
      null
    );
  }
  GetUserDetails(username: string): Observable<UserProfile> {
    return this.http.get<UserProfile>(environment.apiURL + 'Users/' + username);
  }

  SetCurrentUser(userProfileData: UserProfile) {
    this.CurrentUser$.next(userProfileData);
  }
}
