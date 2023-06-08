import { Injectable, NgZone } from '@angular/core';
import { User } from '../../models/UserProfile/user';
import * as auth from 'firebase/auth';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import {
  AngularFirestore,
  AngularFirestoreDocument,
} from '@angular/fire/compat/firestore';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LocalStorageService } from './local-storage.service';
import { JWTTokenService } from './jwt-token.service';
import { UserService } from './user.service';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  userData: any; // Save logged in user data
  constructor(
    public afs: AngularFirestore, // Inject Firestore service
    public afAuth: AngularFireAuth, // Inject Firebase auth service
    public router: Router,
    public ngZone: NgZone, // NgZone service to remove outside scope warning
    private snackBar: MatSnackBar,
    private localStorageService: LocalStorageService,
    private jwtTokenService: JWTTokenService,
    private userService: UserService
  ) {
    /* Saving user data in localstorage when 
    logged in and setting up null when logged out */
    this.afAuth.authState.subscribe((user) => {
      if (user) {
        debugger;
        this.userData = user;
        this.localStorageService.set('user', JSON.stringify(this.userData));
        JSON.parse(this.localStorageService.get('user')!);
        user.getIdToken().then((token) => {
          this.jwtTokenService.setToken(token);
          //auto register or create the user (bare minimum profile) in app db if it doesn't already exist
          this.userService.CreateUserIfDoesNotExist().subscribe(() => {
            this.userService.GetUserDetails(user.uid).subscribe((res) => {
              this.userService.SetCurrentUser(res);
            });
          });
        });
      } else {
        this.localStorageService.set('user', 'null');
        JSON.parse(this.localStorageService.get('user')!);
        this.jwtTokenService.removeToken();
      }
    });
  }
  openSnackBar(message: string, type?: string, durationInSeconds?: number) {
    let panelClass = 'default-snackbar';
    if (type) {
      if (type === 'success') {
        panelClass = 'success-snackbar';
      } else {
        panelClass = 'failure-snackbar';
      }
    }
    if (durationInSeconds) {
      this.snackBar.open(message, 'OK', {
        duration: 1000 * durationInSeconds,
        panelClass: [panelClass, 'login-snackbar'],
      });
    } else {
      this.snackBar.open(message, 'OK', {
        panelClass: [panelClass, 'login-snackbar'],
      });
    }
  }

  // Sign in with email/password
  SignIn(email: string, password: string) {
    return this.afAuth
      .signInWithEmailAndPassword(email, password)
      .then((result) => {
        this.SetUserData(result.user);
        this.afAuth.authState.subscribe((user) => {
          if (user) {
            this.router.navigate(['home']);
          }
        });
      })
      .catch((error) => {
        this.openSnackBar(error.message, 'failure');
      });
  }
  // Sign up with email/password
  SignUp(email: string, password: string) {
    return this.afAuth
      .createUserWithEmailAndPassword(email, password)
      .then((result) => {
        /* Call the SendVerificaitonMail() function when new user sign 
        up and returns promise */
        this.SendVerificationMail();
        this.SetUserData(result.user);
      })
      .catch((error) => {
        this.openSnackBar(error.message, 'failure');
      });
  }
  // Send email verfificaiton when new user sign up
  SendVerificationMail() {
    return this.afAuth.currentUser
      .then((u: any) => u.sendEmailVerification())
      .then(() => {
        this.router.navigate(['verify-email-address']);
      });
  }
  // Reset Forggot password
  ForgotPassword(passwordResetEmail: string) {
    return this.afAuth
      .sendPasswordResetEmail(passwordResetEmail)
      .then(() => {
        this.openSnackBar('Password reset email sent, check your inbox.');
      })
      .catch((error) => {
        this.openSnackBar(error, 'failure');
      });
  }
  // Returns true when user is looged in and email is verified
  get isLoggedIn(): boolean {
    const user = JSON.parse(this.localStorageService.get('user')!);
    return user !== null && user.emailVerified !== false ? true : false;
  }
  // Sign in with Google
  GoogleAuth() {
    return this.AuthLogin(new auth.GoogleAuthProvider()).then((res: any) => {
      this.afAuth.authState.subscribe((user) => {
        if (user) {
          this.router.navigate(['home']);
        }
      });
    });
  }
  // Auth logic to run auth providers
  AuthLogin(provider: any) {
    return this.afAuth
      .signInWithPopup(provider)
      .then((result) => {
        this.SetUserData(result.user);
        this.afAuth.authState.subscribe((user) => {
          if (user) {
            this.router.navigate(['home']);
          }
        });
      })
      .catch((error) => {
        this.openSnackBar(error, 'failure');
      });
  }
  /* Setting up user data when sign in with username/password, 
  sign up with username/password and sign in with social auth  
  provider in Firestore database using AngularFirestore + AngularFirestoreDocument service */
  SetUserData(user: any) {
    const userRef: AngularFirestoreDocument<any> = this.afs.doc(
      `users/${user.uid}`
    );
    const userData: User = {
      uid: user.uid,
      email: user.email,
      displayName: user.displayName,
      photoURL: user.photoURL,
      emailVerified: user.emailVerified,
    };
    return userRef.set(userData, {
      merge: true,
    });
  }
  // Sign out
  SignOut() {
    return this.afAuth.signOut().then(() => {
      this.localStorageService.remove('user');
      this.router.navigate(['sign-in']);
    });
  }
}
