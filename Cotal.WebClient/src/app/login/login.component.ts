import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { AuthenService } from "app/core/services/authen.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  loading = false;
  error = '';
  constructor(private router: Router,
        private authenService: AuthenService) { }

  ngOnInit() {
    this.authenService.logout();
  }
login() {
        this.loading = true;
        this.authenService.login(this.model.username, this.model.password)
            .subscribe(result => {
                if (result === true) {
                    // login successful
                    this.router.navigate(['/admin']);
                } else {
                    // login failed
                    this.error = 'Username or password is incorrect';
                    this.loading = false;
                }
            });
    }
}
