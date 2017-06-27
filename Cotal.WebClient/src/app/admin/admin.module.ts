import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { AdminRouting } from "app/admin/admin.routing";

@NgModule({
  imports: [
    CommonModule,
    AdminRouting
  ],
  declarations: [AdminComponent]
})
export class AdminModule { }
