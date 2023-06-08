import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './components/general/app-layout/app-layout.component';
import { UserHomeComponent } from './components/main-screens/user-home/user-home.component';
import { AdminHomeComponent } from './components/main-screens/admin-home/admin-home.component';
import { PostComponent } from './components/post-content/post/post.component';
import { SubscriptionsComponent } from './components/subscriptions/subscriptions.component';
import { UserProfileComponent } from './components/main-screens/user-profile/user-profile.component';
import { CreatePostComponent } from './components/main-screens/create-post/create-post.component';
import { PaymentComponent } from './components/payment/payment.component';
import { CreatorComponent } from './components/main-screens/creator/creator.component';
import { ForgotPasswordComponent } from './components/firebase/forgot-password/forgot-password.component';
import { SignInComponent } from './components/firebase/sign-in/sign-in.component';
import { SignUpComponent } from './components/firebase/sign-up/sign-up.component';
import { VerifyEmailComponent } from './components/firebase/verify-email/verify-email.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { ErrorComponent } from './components/general/error/error.component';

const routes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      { path: 'sign-in', component: SignInComponent },
      { path: 'register-user', component: SignUpComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'verify-email-address', component: VerifyEmailComponent },
      { path: 'home', component: UserHomeComponent, canActivate: [AuthGuard] },
      { path: '', redirectTo: '/sign-in', pathMatch: 'full' },
      {
        path: 'admin-home',
        component: AdminHomeComponent,
        canActivate: [AuthGuard],
      },
      { path: 'post', component: PostComponent, canActivate: [AuthGuard] },
      {
        path: 'subscriptions',
        component: SubscriptionsComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'user-profile',
        component: UserProfileComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'create-post',
        component: CreatePostComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'make-payment',
        component: PaymentComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'creator/:username',
        component: CreatorComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'error',
        component: ErrorComponent,
        canActivate: [AuthGuard],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
