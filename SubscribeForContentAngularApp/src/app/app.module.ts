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
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
