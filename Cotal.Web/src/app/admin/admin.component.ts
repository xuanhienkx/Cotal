import { Component, OnInit } from '@angular/core';    

import { SystemConstants } from 'app/core/common/system.constants';

import { UrlConstants } from 'app/core/common/url.constants';

import { UtilityService } from 'app/core/services/utility.service';

import { AuthenService } from 'app/core/services/authen.service';

import { LoggedInUser } from 'app/core/models/loggedinUser';  
@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  public baseFolder: string = SystemConstants.BASE_API;
  public user: LoggedInUser;
  constructor(private utilityService: UtilityService, private authenService: AuthenService) { }


  ngOnInit() {
    this.user = this.authenService.getLoggedInUser(); 
  }
  logout() {
    localStorage.removeItem(SystemConstants.CURRENT_USER);
    this.utilityService.navigate(UrlConstants.LOGIN);
  }
}
