import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions, RequestOptionsArgs,ResponseContentType } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

@Injectable()
export class AuthenService {
    public token: string;
    constructor(private http: Http) {
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
    }
    login(username: string, password: string): Observable<boolean> {
        let body = JSON.stringify({ userName: 'admin', password: 'Phap@3658!$', rememberMe: true }) 
        let headers = new Headers();
        headers.append("Content-Type", "application/json; charset=utf-8"); 
        console.log(body); 
        let options = new RequestOptions({headers: headers,method: "post"});
        return this.http.post('http://localhost:5838/api/Account/token',body, options)
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                console.log(response)
                let token = response.json() && response.json().token;
                if (token) {
                    // set token property
                    this.token = token;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
    }

}
