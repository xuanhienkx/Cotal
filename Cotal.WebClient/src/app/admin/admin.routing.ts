 
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from "app/admin/admin.component";
const routes: Routes = [
    { path: '', redirectTo: 'index', pathMatch: 'full' },
    { path: 'index', component: AdminComponent }
]; 
export const AdminRouting = RouterModule.forChild(routes);