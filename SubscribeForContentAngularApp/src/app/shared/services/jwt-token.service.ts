import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { LocalStorageService } from './local-storage.service';

@Injectable()
export class JWTTokenService {
  jwtToken: string;
  decodedToken: any;

  constructor(private localStorageService: LocalStorageService) {
    this.jwtToken = '';
  }

  setToken(token: string) {
    if (token) {
      this.jwtToken = token;
      this.localStorageService.set('userIdToken', token);
    }
  }

  removeToken() {
    this.localStorageService.remove('userIdToken');
  }

  decodeToken() {
    if (this.jwtToken) {
      this.decodedToken = jwt_decode(this.jwtToken);
    }
  }

  getToken() {
    return this.localStorageService.get('userIdToken');
  }

  getDecodeToken() {
    return jwt_decode(this.jwtToken);
  }

  getUser() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken['displayname'] : null;
  }

  getEmailId() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken['email'] : null;
  }

  getExpiryTime() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken['exp'] : null;
  }

  isTokenExpired(): boolean {
    const expiryTime = this.getExpiryTime();
    if (expiryTime) {
      const expiryTimeVal = Number(expiryTime);
      return 1000 * expiryTimeVal - new Date().getTime() < 5000;
    } else {
      return false;
    }
  }
}
