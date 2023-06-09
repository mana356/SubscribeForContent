import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserProfile } from 'src/app/models/UserProfile/user-profile.model';
import { UserProfileUpdateDto } from 'src/app/models/UserProfile/user-profile-update.model';

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

  UpdateUserProfile(profileData: UserProfileUpdateDto): Observable<any> {
    let headers = new HttpHeaders();
    headers.set('Content-Type', 'null');
    headers.set('Accept', 'multipart/form-data');
    let params = new HttpParams();
    debugger;
    var formData: any = new FormData();
    formData.append('Name', profileData.Name);
    formData.append('UserName', profileData.UserName);
    formData.append('DateOfBirth', profileData.DateOfBirth);
    formData.append('Bio', profileData.Bio ?? '');
    formData.append('IsACreator', profileData.IsACreator);
    if (profileData.ProfilePicture !== undefined) {
      formData.append(
        'ProfilePicture',
        profileData.ProfilePicture,
        profileData.ProfilePicture.name
      );
    }
    if (profileData.CoverPicture !== undefined) {
      formData.append(
        'CoverPicture',
        profileData.CoverPicture,
        profileData.CoverPicture.name
      );
    }

    return this.http.put<any>(environment.apiURL + 'Users', formData, {
      params: params,
    });
  }
}
