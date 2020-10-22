import { Router } from '@angular/router';
import { CookieStoreService } from './../CookieStore.service';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import {  catchError, tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export class GlobalHttpInterceptor implements HttpInterceptor {
  constructor(private cookieStoreService: CookieStoreService,
              private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.cookieStoreService.user != null ? this.cookieStoreService.user.token : null;

    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
        'content-type': 'application/json; charset=utf-8'
      }
    });

    // TODO on loader

    return next.handle(req).pipe(
      tap((resp) => {
        if (resp.type !== 0) {
          // TODO: off loader
        }
      }),
      catchError((error, caught) => {
        const message = this.processError(error);
        // TODO: show loader
        // TODO: off loader

        throw error;
      })
    );
  }

  private processError(error): string {
    let result = '';

    if (error) {
      if (error.status === 500) {
        result = error.message;
      } else if (error.status === 0) {
        result = 'Some error on server. Try later';
      } else if (error.status === 401) {
        result = 'Access denied';
        this.cookieStoreService.user = null;
        this.router.navigateByUrl('/auth');
      }
    }

    return result;
  }
}
