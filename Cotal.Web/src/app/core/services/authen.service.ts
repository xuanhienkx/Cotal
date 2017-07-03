import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { LoggedInUser } from "app/core/models/LoggedInUser";
import { SystemConstants } from "app/core/common/system.constants";
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthenService {
  constructor(private _http: Http) {
  }
  login(userName: string, password: string) {

    let headers = new Headers();
    headers.append("Content-Type", "application/x-www-form-urlencoded");
    let options = new RequestOptions({ headers: headers });
    let url = '/token';
    let body = "username=" +
      encodeURIComponent(userName) +
      "&password=" +
      encodeURIComponent(password);

    return this._http.post(url, body, options).map((response: Response) => {
      let user: LoggedInUser = response.json(); 
      if (user && user.access_token) {
        localStorage.setItem(SystemConstants.CURRENT_USER, JSON.stringify(user));
      }

    });
  }
  logout() {
    localStorage.removeItem(SystemConstants.CURRENT_USER);
  }
  isUserAuthenticated(): boolean {
    let user = localStorage.getItem(SystemConstants.CURRENT_USER);
    if (user != null) {
      return true;
    }
    else
      return false;
  }
  getLoggedInUser(): LoggedInUser {
    let user: LoggedInUser;
    if (this.isUserAuthenticated()) {
      var userData = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER));
      user = new LoggedInUser(userData.access_token, userData.UserName, userData.ExpiresIn,
        userData.FullName, userData.Email, userData.Avatar, userData.Permission, userData.Roles);
    }
    else
      user = null;
    return user;
  }
}
