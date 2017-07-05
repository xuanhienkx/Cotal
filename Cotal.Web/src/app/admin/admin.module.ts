import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { AdminRoutingModule } from "app/admin/admin.routung";
import { SidebarMenuComponent } from "app/shared/sidebar-menu/sidebar-menu.component";  
import { TopMenuComponent } from "app/shared/top-menu/top-menu.component";     

import { UtilityService } from 'app/core/services/utility.service';
import { AuthenService } from 'app/core/services/authen.service';
import { DataService } from 'app/core/services/data.service';
import { NotificationService } from 'app/core/services/notification.service';
import { UploadService } from "app/core/services/upload.service";

@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule
  ],
  declarations: [AdminComponent, SidebarMenuComponent, TopMenuComponent],
  providers: [UtilityService, AuthenService, DataService, NotificationService,UploadService]
})
export class AdminModule { }
