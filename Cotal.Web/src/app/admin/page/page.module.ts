import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageComponent } from './page.component';
import { Routes, RouterModule } from "@angular/router";
import { PaginationModule, ModalModule } from "ngx-bootstrap";
import { FormsModule } from "@angular/forms/";
import { DataService } from "app/core/services/data.service";
import { UtilityService } from "app/core/services/utility.service";
import { UploadService } from "app/core/services/upload.service"; 
//import { SimpleTinyComponent } from "app/shared/simple-tiny/simple-tiny.component";
const routes: Routes = [
    { path: '', redirectTo: 'index', pathMatch: 'full' },
    { path: 'index', component: PageComponent }
];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    PaginationModule.forRoot(),
    FormsModule,
    ModalModule.forRoot()
  ], providers: [DataService, UtilityService, UploadService],
  declarations: [PageComponent]
})
export class PageModule { }
