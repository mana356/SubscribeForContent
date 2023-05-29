import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { AuthService } from '../services/auth.service';
import { JWTTokenService } from '../services/jwt-token.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private authTokenService: JWTTokenService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // add auth header with jwt if account is logged in and request is to the api url
    const isLoggedIn = this.authService.isLoggedIn;
    const isApiUrl = request.url.startsWith(environment.apiURL);
    if (isLoggedIn && isApiUrl) {
      if (!this.authTokenService.isTokenExpired()) {
        const token = this.authTokenService.getToken();
        request = request.clone({
          setHeaders: { Authorization: `Bearer ${token}` },
        });
      } else {
        this.authService.userData.getIdToken().then((token: string) => {
          this.authTokenService.setToken(token);

          request = request.clone({
            setHeaders: { Authorization: `Bearer ${token}` },
          });
        });
      }
    }

    return next.handle(request);
  }
}
