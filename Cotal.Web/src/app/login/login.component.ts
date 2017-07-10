import { Component, OnInit } from '@angular/core';
import { AuthenService } from "app/core/services/authen.service";
import { Router } from "@angular/router";
import { NotificationService } from "app/core/services/notification.service";
import { UrlConstants } from "app/core/common/url.constants";
import { MessageContstants } from "app/core/common/message.constants";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loading = false;
  model: any = {}; 
  returnUrl: string; 
  constructor(private authenService: AuthenService,
    private notificationService: NotificationService,
    private router: Router) { 
      this.authenService.logout();
    }

  ngOnInit() {
  }
  login() {
    this.loading = true; 
    this.authenService.login(this.model.username, this.model.password).subscribe(data => {
      this.router.navigate([UrlConstants.ADMIN]);
    }, error => {
      this.notificationService.printErrorMessage(MessageContstants.SYSTEM_ERROR_MSG);
      this.loading = false;
    });
  }
}
