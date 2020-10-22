import { UserVmModel } from './../Models/UserVm.model';
import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';

@Injectable()
export class CookieStoreService {
  private userCookieKey = 'user_652D9C28-8E9C-4CB9-8AEE-DFBC95B2C6E5';

  /* bad idea */
  private subsciptionChangeUser: any;
  /* bad idea */

  public constructor(private cookieService: CookieService) {
  }

  /* bad idea */
  public subscribeOnUser(subscription): void {
    this.subsciptionChangeUser = subscription;
  }
  /* bad idea */

  public get user() {
    const result = this.getValue<UserVmModel>(this.userCookieKey);

    return result;
  }

  public set user(value: UserVmModel) {
    /* bad idea */
    if (this.subsciptionChangeUser != null
      && typeof this.subsciptionChangeUser === 'function') {
      this.subsciptionChangeUser(value);
    }
    /* bad idea */

    if (this.cookieService.check(this.userCookieKey)) {
      this.cookieService.delete(this.userCookieKey);
    }

    if (value != null) {
      const json = JSON.stringify(value);

      this.setValue(this.userCookieKey, json);
    } else {
      document.cookie = '';
    }
  }

  private getValue<T>(name: string) {
    const json = this.cookieService.get(name);

    const result: T = json != null && json !== '' ? JSON.parse(json) : null;

    return result;
  }

  private setValue(name: string, value: string) {
    if (this.cookieService.check(name)) {
      this.cookieService.delete(name);
    }

    this.cookieService.set(name, value, 365, '/');
  }
}
