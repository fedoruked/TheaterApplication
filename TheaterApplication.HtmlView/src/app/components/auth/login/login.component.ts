import { CookieStoreService } from './../../../services/CookieStore.service';
import { UserService } from './../../../services/User.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

// TODO validation

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public email: string;
  public password: string;

  constructor(private userService: UserService,
              private cookieStoreService: CookieStoreService,
              private router: Router) { }

  ngOnInit() {
  }

  // TODO click by press enter
  public onLogin() {
    this.userService.login(this.email, this.password).then((user) => {
      this.cookieStoreService.user = user;

      // TODO: member entered location and return to it
      this.router.navigateByUrl('/');
    });
  }

}
