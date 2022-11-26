import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot  } from '@angular/router'
import { Observable } from 'rxjs';
import { AuthService } from '@app/core/services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private router: Router,
      ) {}

      canActivate(routeSnapshot: ActivatedRouteSnapshot): Observable<boolean> {
        let redirect = routeSnapshot.data['authGuardRedirect'];
        let isUserLoggedIn = this.authService.isLoggedIn();

        return isUserLoggedIn;
  }
}