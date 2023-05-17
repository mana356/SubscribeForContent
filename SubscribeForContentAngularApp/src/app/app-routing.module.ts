import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './components/app-layout/app-layout.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { PostComponent } from './components/post/post.component';
import { SubscriptionsComponent } from './components/subscriptions/subscriptions.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { CreatePostComponent } from './components/create-post/create-post.component';
import { PaymentComponent } from './components/payment/payment.component';
import { CreatorComponent } from './components/creator/creator.component';

const routes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      { path: 'home', component: UserHomeComponent },
      { path: '', redirectTo: 'home', pathMatch: 'prefix' },
      { path: 'admin-home', component: AdminHomeComponent },
      { path: 'post', component: PostComponent },
      { path: 'subscriptions', component: SubscriptionsComponent },
      { path: 'user-profile', component: UserProfileComponent },
      { path: 'create-post', component: CreatePostComponent },
      { path: 'make-payment', component: PaymentComponent },
      { path: 'creator/:username', component: CreatorComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
