import {
  trigger,
  state,
  style,
  transition,
  animate,
} from '@angular/animations';
import { Component } from '@angular/core';
import { UserProfile } from 'src/app/models/UserProfile/user-profile.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-app-layout',
  templateUrl: './app-layout.component.html',
  styleUrls: ['./app-layout.component.scss'],
  animations: [
    trigger('hamburguerX', [
      /*
        state hamburguer => is the regular 3 lines style.
        states topX, hide, and bottomX => used to style the X element
      */
      state('hamburguer', style({})),
      // style top bar to create the X
      state(
        'topX',
        style({
          transform: 'rotate(45deg)',
          transformOrigin: 'left',
          margin: '6px 6px 6px 4px',
        })
      ),
      // hides element when create the X (used in the middle bar)
      state(
        'hide',
        style({
          opacity: 0,
        })
      ),
      // style bottom bar to create the X
      state(
        'bottomX',
        style({
          transform: 'rotate(-45deg)',
          transformOrigin: 'left',
          margin: '6px 6px 6px 4px',
        })
      ),
      transition('* => *', [
        animate('0.2s'), // controls animation speed
      ]),
    ]),
  ],
})
export class AppLayoutComponent {
  currentUserProfile: UserProfile | undefined;
  isHamburguer = true;

  constructor(
    public authService: AuthService,
    private userService: UserService
  ) {
    this.authService.afAuth.authState.subscribe((user) => {
      if (user) {
        this.userService.CurrentUserProfile.subscribe((res) => {
          this.currentUserProfile = res;
        });
      }
    });
  }

  onClick() {
    this.isHamburguer = !this.isHamburguer;
  }
}
