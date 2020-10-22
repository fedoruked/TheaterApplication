import { CookieStoreService } from './../services/CookieStore.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class IsAdminGuard implements CanActivate {
  constructor(private cookieStoreService: CookieStoreService,
              private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const user = this.cookieStoreService.user;
    let result = false;

    if (user != null && user.token != null && user.roles != null) {
      user.roles.forEach(role => {
        if (role === 'admin') {
          result = true;
        }
      });
    }

    if (!result) {
      this.router.navigateByUrl(`/auth`);
    }

    return result;
  }
}
