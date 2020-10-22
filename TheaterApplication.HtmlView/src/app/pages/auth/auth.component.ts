import { Router } from '@angular/router';
import { CookieStoreService } from './../../services/CookieStore.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {

  constructor(private cookieStoreService: CookieStoreService,
              private router: Router) { }

  ngOnInit() {
    if (this.cookieStoreService.user != null) {
      this.router.navigateByUrl('/');
    }
  }
}
