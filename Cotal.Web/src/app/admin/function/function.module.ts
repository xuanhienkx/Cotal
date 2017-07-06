import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FunctionComponent } from './function.component';
import { Routes, RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { ModalModule } from "ngx-bootstrap"; 
import { TreeModule } from 'angular-tree-component';
const functionRoutes: Routes = [
  //localhost:4200/main/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:4200/main/user/index
  { path: 'index', component: FunctionComponent }
]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(functionRoutes),
    TreeModule,
    FormsModule,
    ModalModule.forRoot()
  ],
  declarations: [FunctionComponent]
})
export class FunctionModule { }
