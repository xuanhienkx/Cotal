import { NgModule } from '@angular/core'; 
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';  
import { FormsModule } from '@angular/forms';  
import { DataService } from 'app/core/services/data.service';
import { NotificationService } from 'app/core/services/notification.service'; 
import { PaginationModule, ModalModule } from 'ngx-bootstrap';
import { MultiselectDropdownModule } from "angular-2-dropdown-multiselect";
import { Daterangepicker } from 'ng2-daterangepicker';
const userRoutes: Routes = [
  //localhost:4200/admin/user
  { path: '', redirectTo: 'index', pathMatch: 'full' },
  //localhost:4200/admin/user/index
  { path: 'index', component: UserComponent }
]
@NgModule({
  imports: [
    CommonModule,
    PaginationModule.forRoot(), 
    FormsModule,
    MultiselectDropdownModule,
    ModalModule.forRoot(),
    Daterangepicker,
    RouterModule.forChild(userRoutes)
  ],
  providers: [DataService, NotificationService],
  declarations: [UserComponent]
})
export class UserModule { }
