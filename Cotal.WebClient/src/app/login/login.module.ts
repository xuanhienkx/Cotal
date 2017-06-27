import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { Routes,RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AuthenService } from "app/core/services/authen.service";
export const routes: Routes = [
  //localhost:4200/login
  { path: '', component: LoginComponent }
];
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],
   providers: [AuthenService],
  declarations: [LoginComponent]
})
export class LoginModule { }
