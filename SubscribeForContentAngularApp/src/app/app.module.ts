import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppLayoutComponent } from './components/general/app-layout/app-layout.component';
import { UserHomeComponent } from './components/main-screens/user-home/user-home.component';
import { AdminHomeComponent } from './components/main-screens/admin-home/admin-home.component';
import { CreatePostComponent } from './components/main-screens/create-post/create-post.component';
import { UserProfileComponent } from './components/main-screens/user-profile/user-profile.component';
import { SubscribeToCreatorComponent } from './components/main-screens/subscribe-to-creator/subscribe-to-creator.component';
import { PaymentComponent } from './components/payment/payment.component';
import { PostComponent } from './components/post-content/post/post.component';
import { MaterialModule } from './material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DatePipe } from '@angular/common';
import { CreatorComponent } from './components/main-screens/creator/creator.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DndDirective } from './shared/directives/dnd.directive';
import { ProgressComponent } from './components/general/progress/progress.component';
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
import { SpinnerComponent } from './components/general/spinner/spinner.component';
import { LoadingInterceptor } from './shared/interceptors/loading.interceptor';
import { ErrorComponent } from './components/general/error/error.component';
import { JWTTokenService } from './shared/services/jwt-token.service';
import { LoaderService } from './shared/services/loader.service';
import { FileContentComponent } from './components/post-content/file-content/file-content.component';
import { NgImageSliderModule } from 'ng-image-slider';
import {
  JsonDateParser,
  JsonParser,
  JsonParserHttpInterceptor,
} from './shared/interceptors/json.interceptor';
import { CommentComponent } from './components/general/comment/comment.component';
import { PostDetailComponent } from './components/main-screens/post-detail/post-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    AppLayoutComponent,
    UserHomeComponent,
    AdminHomeComponent,
    CreatePostComponent,
    UserProfileComponent,
    SubscribeToCreatorComponent,
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
    FileContentComponent,
    CommentComponent,
    PostDetailComponent,
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
    NgImageSliderModule,
  ],
  providers: [
    DatePipe,
    AuthService,
    { provide: JsonParser, useClass: JsonDateParser },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JsonParserHttpInterceptor,
      multi: true,
    },
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
