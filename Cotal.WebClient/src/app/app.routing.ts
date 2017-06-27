import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from "app/core/guards/auth.guard";


const appRoutes: Routes = [
    //localhost:4200
    { path: '', redirectTo: 'main', pathMatch: 'full' },
    //localhost:4200/login
    { path: 'login', loadChildren: './login/login.module#LoginModule' },
    //localhost:4200/admin
    { path: 'admin', loadChildren: './admin/admin.module#AdminModule' },
    //localhost:4200/main
    { path: 'main', loadChildren: './main/main.module#MainModule' },
 
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
 
export const routing = RouterModule.forRoot(appRoutes);