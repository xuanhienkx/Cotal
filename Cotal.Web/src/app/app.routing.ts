import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from "app/core/guards/auth.guard";

const routes: Routes = [
  // {
  //   path: '',
  //   children: []
  // }
  //localhost:4200
    { path: '', redirectTo: 'main', pathMatch: 'full' },
    { path: 'main', loadChildren: './main/main.module#MainModule' },
    //localhost:4200/login
    { path: 'login', loadChildren: './login/login.module#LoginModule' },
    //localhost:4200/main
    { path: 'admin', loadChildren: './admin/admin.module#AdminModule',canActivate:[AuthGuard] }
   // { path: 'main', loadChildren: './main/main.module#MainModule',canActivate:[AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
