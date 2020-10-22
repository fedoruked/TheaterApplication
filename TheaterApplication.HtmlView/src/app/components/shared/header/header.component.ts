import { Router } from '@angular/router';
import { CookieStoreService } from './../../../services/CookieStore.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  public hasUser: boolean;

  constructor(private cookieStoreService: CookieStoreService,
              private router: Router) { }

  ngOnInit() {
    this.hasUser = this.cookieStoreService.user != null;

    this.cookieStoreService.subscribeOnUser((user) => {
      this.hasUser = user != null;
    });
  }

  public onLogout(): void {
    this.cookieStoreService.user = null;
  }

}
