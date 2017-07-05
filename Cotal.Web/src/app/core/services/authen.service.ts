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
        userData.FullName, userData.Email, userData.Avatar, userData.Permissions, userData.Roles);
    }
    else
      user = null;
    return user;
  }
  checkAccess(functionId: string) {
    var user = this.getLoggedInUser();
    var result: boolean = false;
    var permission: any[] = JSON.parse(user.Permissions);
    var roles: any[] = JSON.parse(user.Roles);
    var hasPermission: number = permission.findIndex(x => x.FunctionId == functionId && x.CanRead == true);
    if (hasPermission != -1 || roles.findIndex(x => x == "Administrator") != -1) {
      return true;
    }
    else
      return false;
  }
  hasPermission(functionId: string, action: string): boolean {
    var user = this.getLoggedInUser();
    var result: boolean = false;
    var permission: any[] = [];
    if (user.Permissions) {
      user.Permissions = JSON.parse(user.Permissions);
    }
    var roles: any[] = JSON.parse(user.Roles);
    switch (action) {
      case 'create':
        var hasPermission: number = permission.findIndex(x => x.FunctionId == functionId && x.CanCreate == true);
        if (hasPermission != -1 || roles.findIndex(x => x == "Administrator") != -1)
          result = true;
        break;
      case 'update':
        var hasPermission: number = permission.findIndex(x => x.FunctionId == functionId && x.CanUpdate == true);
        if (hasPermission != -1 || roles.findIndex(x => x == "Administrator") != -1)
          result = true;
        break;
      case 'delete':
        var hasPermission: number = permission.findIndex(x => x.FunctionId == functionId && x.CanDelete == true);
        if (hasPermission != -1 || roles.findIndex(x => x == "Administrator") != -1)
          result = true;
        break;
    }
    return result;
  }
}
