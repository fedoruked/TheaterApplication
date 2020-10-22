import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieStoreService } from 'src/app/services/CookieStore.service';
import { UserService } from 'src/app/services/User.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public email: string;
  public password: string;

  constructor(private userService: UserService,
              private cookieStoreService: CookieStoreService,
              private router: Router) { }

  ngOnInit() {
  }

  // TODO click by press enter
  public onLogin() {
    this.userService.register(this.email, this.password).then((user) => {
      this.userService.userEmail = this.email;
      // TODO: member entered location and return to it
      this.router.navigateByUrl('/auth/registration/code');
    });
  }

}
