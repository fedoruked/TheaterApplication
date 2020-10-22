import { UserVmModel } from './../Models/UserVm.model';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class UserService {
  private basePath: string;

  public userEmail: string;

  constructor(private httpClient: HttpClient) {
    this.basePath = `${environment.basePath}users`;
  }

  public login(email: string, password: string): Promise<UserVmModel> {
    const body = {
      email,
      password
    };

    const result = this.httpClient.post<UserVmModel>(`${this.basePath}/login`, body).toPromise();

    return result;
  }

  public register(email: string, password: string): Promise<UserVmModel> {
    const body = {
      email,
      password
    };

    const result = this.httpClient.post<UserVmModel>(`${this.basePath}/register`, body).toPromise();

    return result;
  }

  public approve(approveCode: string): Promise<UserVmModel> {
    const body = {
      email: this.userEmail,
      approveCode
    };

    const result = this.httpClient.post<UserVmModel>(`${this.basePath}/approve`, body).toPromise();

    return result;
  }
}
