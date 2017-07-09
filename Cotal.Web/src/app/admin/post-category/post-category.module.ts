import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from "@angular/router"; 
import { PostCategoryComponent } from "./post-category.component";
import { TreeModule } from "angular-tree-component/";
import { ModalModule } from "ngx-bootstrap";
import { FormsModule } from "@angular/forms";
import { DataService } from "app/core/services/data.service";
import { UtilityService } from "app/core/services/utility.service";
const routes: Routes = [
    { path: '', redirectTo: 'index', pathMatch: 'full' },
    { path: 'index', component: PostCategoryComponent }
];
@NgModule({
  imports: [
    CommonModule,
    TreeModule,
    ModalModule.forRoot(),
    FormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [PostCategoryComponent],
  providers: [DataService, UtilityService]
})
export class PostCategoryModule { }
