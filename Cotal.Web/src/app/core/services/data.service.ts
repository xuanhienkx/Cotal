import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Router } from '@angular/router';
import { SystemConstants } from './../common/system.constants';
import { AuthenService } from './authen.service';
import { NotificationService } from './notification.service';
import { UtilityService } from './utility.service'; 
import { Observable } from 'rxjs/Observable';
import { MessageContstants } from './../common/message.constants';

@Injectable()
export class DataService { 
   
  private Bearer: string;
  constructor(private _http: Http, private _router: Router, private _authenService: AuthenService,
    private _notificationService: NotificationService, private _utilityService: UtilityService) {
 
  }
  private jwt() {
    // create authorization header with jwt token
    let currentUser = JSON.parse(localStorage.getItem(SystemConstants.CURRENT_USER)); 
    if (currentUser && currentUser.access_token) {
      let headersA = new Headers({ 'Authorization': 'Bearer ' + currentUser.access_token }); 
      headersA.append('content-Type', 'application/json; charset=utf-8');
      return new RequestOptions({ headers: headersA });
    }
  }
  get(uri: string) {
     
    return this._http.get(SystemConstants.BASE_API + uri, this.jwt()).map(this.extractData);
  }
  post(uri: string, data?: any) {    
    console.log(data);
    return this._http.post(SystemConstants.BASE_API + uri, data, this.jwt()).map(this.extractData);
  }
  put(uri: string, data?: any) {
   
    return this._http.put(SystemConstants.BASE_API + uri, data, this.jwt()).map(this.extractData);
  }
  delete(uri: string, key: string, id: string) {
    
    return this._http.delete(SystemConstants.BASE_API + uri + "/?" + key + "=" + id, this.jwt())
      .map(this.extractData);
  }
  deleteWithMultiParams(uri: string, params) {      
    var paramStr: string = '';
    for (let param in params) {
      paramStr += param + "=" + params[param] + '&';
    }
    return this._http.delete(SystemConstants.BASE_API + uri + "/?" + paramStr, this.jwt())
      .map(this.extractData);

  }
  postFile(uri: string, data?: any) {
    let newHeader = new Headers();
    newHeader.append("Authorization", this.Bearer + this._authenService.getLoggedInUser().access_token);
    return this._http.post(SystemConstants.BASE_API + uri, data, { headers: newHeader })
      .map(this.extractData);
  }
  private extractData(res: Response) {
    //console.log(res);
    let body = res.json();
    return body || {};
  }
  public handleError(error: any) {
    if (error.status == 401) {
      localStorage.removeItem(SystemConstants.CURRENT_USER);
      this._notificationService.printErrorMessage(MessageContstants.LOGIN_AGAIN_MSG);
      this._utilityService.navigateToLogin();
    }
    else if (error.status == 403) {
      localStorage.removeItem(SystemConstants.CURRENT_USER);
      this._notificationService.printErrorMessage(MessageContstants.FORBIDDEN);
      this._utilityService.navigateToLogin();
    }
    else {
      let errMsg = JSON.parse(error._body).Message;
      this._notificationService.printErrorMessage(errMsg);

      return Observable.throw(errMsg);
    }         
  }
}
