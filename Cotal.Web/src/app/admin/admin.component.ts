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
   // this.loadScript();
    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "../../assets/js/custom.js";
    this.elementRef.nativeElement.appendChild(s);
  }
  logout() {
    localStorage.removeItem(SystemConstants.CURRENT_USER);
    this.utilityService.navigate(UrlConstants.LOGIN);
  }
  loadScript(){
      //$.getScript('../js/mimity.js'); 
      $.getScript("../../../node_modules/fastclick/lib/fastclick.js",
            "../../../node_modules/alertifyjs/build/alertify.js",
            "../../../node_modules/moment/moment.js",
            "../../../node_modules/tinymce/tinymce.js",
            "../../../node_modules/tinymce/themes/modern/theme.js",
            "../../../node_modules/tinymce/plugins/link/plugin.js",
            "../../../node_modules/tinymce/plugins/paste/plugin.js",
            "../../../node_modules/tinymce/plugins/table/plugin.js",
            "../../../node_modules/tinymce/plugins/autosave/plugin.js",
            "../../../node_modules/tinymce/plugins/autolink/plugin.js",
            "../../../node_modules/tinymce/plugins/code/plugin.js",
            "../../../node_modules/tinymce/plugins/codesample/plugin.js",
            "../../../node_modules/tinymce/plugins/colorpicker/plugin.js",
            "../../../node_modules/tinymce/plugins/emoticons/plugin.js",
            "../../../node_modules/tinymce/plugins/fullscreen/plugin.js",
            "../../../node_modules/tinymce/plugins/hr/plugin.js",
            "../../../node_modules/tinymce/plugins/image/plugin.js",
            "../../../node_modules/tinymce/plugins/imagetools/plugin.js",
            "../../../node_modules/tinymce/plugins/media/plugin.js",
            "../../../node_modules/tinymce/plugins/preview/plugin.js",
            "../../../node_modules/tinymce/plugins/wordcount/plugin.js",
            "../../../node_modules/tinymce/plugins/textcolor/plugin.js",
            "../../../node_modules/chart.js/dist/chart.js")
  }
}
