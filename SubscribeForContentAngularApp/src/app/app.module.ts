import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppLayoutComponent } from './components/app-layout/app-layout.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { CreatePostComponent } from './components/create-post/create-post.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { SubscriptionsComponent } from './components/subscriptions/subscriptions.component';
import { PaymentComponent } from './components/payment/payment.component';
import { PostComponent } from './components/post/post.component';
import { MaterialModule } from './material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DatePipe } from '@angular/common';
import { CreatorComponent } from './components/creator/creator.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DndDirective } from './shared/dnd.directive';
import { ProgressComponent } from './components/progress/progress.component';
import { AngularFireModule } from '@angular/fire/compat';
import { AngularFireAuthModule } from '@angular/fire/compat/auth';
import { AngularFireStorageModule } from '@angular/fire/compat/storage';
import { AngularFirestoreModule } from '@angular/fire/compat/firestore';
import { AngularFireDatabaseModule } from '@angular/fire/compat/database';
import { environment } from '../environments/environment';
import { SignInComponent } from './components/firebase/sign-in/sign-in.component';
import { SignUpComponent } from './components/firebase/sign-up/sign-up.component';
import { ForgotPasswordComponent } from './components/firebase/forgot-password/forgot-password.component';
import { VerifyEmailComponent } from './components/firebase/verify-email/verify-email.component';
import { AuthService } from './shared/services/auth.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './shared/interceptors/jwt.interceptor';
import { LocalStorageService } from './shared/services/local-storage.service';
import { ErrorCatchingInterceptor } from './shared/interceptors/error-catching.interceptor';
import { SpinnerComponent } from './components/shared/spinner/spinner.component';
import { LoadingInterceptor } from './shared/interceptors/loading.interceptor';
import { ErrorComponent } from './components/error/error.component';
import { JWTTokenService } from './shared/services/jwt-token.service';
import { LoaderService } from './shared/services/loader.service';

@NgModule({
  declarations: [
    AppComponent,
    AppLayoutComponent,
    UserHomeComponent,
    AdminHomeComponent,
    CreatePostComponent,
    UserProfileComponent,
    SubscriptionsComponent,
    PaymentComponent,
    PostComponent,
    CreatorComponent,
    DndDirective,
    ProgressComponent,
    SignInComponent,
    SignUpComponent,
    ForgotPasswordComponent,
    VerifyEmailComponent,
    SpinnerComponent,
    ErrorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    BrowserAnimationsModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFireAuthModule,
    AngularFirestoreModule,
    AngularFireStorageModule,
    AngularFireDatabaseModule,
    HttpClientModule,
  ],
  providers: [
    DatePipe,
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorCatchingInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    LocalStorageService,
    JWTTokenService,
    LoaderService,
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
