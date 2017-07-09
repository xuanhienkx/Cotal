import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from './post.component';
import { Routes, RouterModule } from "@angular/router";
import { MultiselectDropdownModule } from "angular-2-dropdown-multiselect";
import { Daterangepicker } from "ng2-daterangepicker";
import { ModalModule,PaginationModule } from "ngx-bootstrap";
import { DataService } from "app/core/services/data.service";
import { UtilityService } from "app/core/services/utility.service";
import { UploadService } from "app/core/services/upload.service"; 
import { FormsModule } from "@angular/forms"; 
import { SimpleTinyModule } from "app/shared/simple-tiny/simple-tiny.module";
const routes: Routes = [
    { path: '', redirectTo: 'index', pathMatch: 'full' },
    { path: 'index', component: PostComponent }
];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    PaginationModule.forRoot(),
    FormsModule,
    ModalModule.forRoot(),
    Daterangepicker,
    MultiselectDropdownModule,
    SimpleTinyModule//.forRoot()
  ], providers: [DataService, UtilityService, UploadService],
  declarations: [PostComponent]
})
export class PostModule { }
