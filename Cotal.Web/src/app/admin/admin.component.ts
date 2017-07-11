import { Component, OnInit, AfterViewChecked, ElementRef, Inject } from '@angular/core'; 
import { SystemConstants } from 'app/core/common/system.constants'; 
import { UrlConstants } from 'app/core/common/url.constants'; 
import { UtilityService } from 'app/core/services/utility.service'; 
import { AuthenService } from 'app/core/services/authen.service'; 
import { LoggedInUser } from 'app/core/models/loggedinUser';
import { DOCUMENT } from "@angular/platform-browser";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit, AfterViewChecked {
  public baseFolder: string = SystemConstants.BASE_API;
  public user: LoggedInUser;
  constructor(private utilityService: UtilityService, 
              private authenService: AuthenService, 
              private elementRef: ElementRef,
              @Inject(DOCUMENT) private document: Document) { }


  ngOnInit() {
    
    this.document.body.classList.remove('login'); 
    this.document.body.classList.add('nav-md'); 
    this.user = this.authenService.getLoggedInUser();
  }
  ngAfterViewChecked() {
    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "../../assets/js/custom.js";
    this.elementRef.nativeElement.appendChild(s);
  }
  logout() {
    localStorage.removeItem(SystemConstants.CURRENT_USER);
    this.utilityService.navigate(UrlConstants.LOGIN);
  }
}
