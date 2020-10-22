import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieStoreService } from 'src/app/services/CookieStore.service';
import { UserService } from 'src/app/services/User.service';

@Component({
  selector: 'app-code-verify',
  templateUrl: './code-verify.component.html',
  styleUrls: ['./code-verify.component.scss']
})
export class CodeVerifyComponent implements OnInit {

  private email: string;
  public code: string;

  constructor(private userService: UserService,
              private cookieStoreService: CookieStoreService,
              private router: Router) { }

  ngOnInit() {
  }

  // TODO click by press enter
  public onLogin() {
    this.userService.approve(this.code).then((user) => {
      this.cookieStoreService.user = user;

      // TODO: member entered location and return to it
      this.router.navigateByUrl('/');
    });
  }

}
