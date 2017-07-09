import { NgModule } from '@angular/core';
import { Routes,RouterModule } from "@angular/router";
import { AdminComponent } from "app/admin/admin.component";

export const adminRouting: Routes = [
    {
        path: '', component: AdminComponent, children: [
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            //localhost:4200/main/home
            { path: 'home', loadChildren: './home/home.module#HomeModule' },
            { path: 'user', loadChildren: './user/user.module#UserModule' },
            { path: 'role', loadChildren: './role/role.module#RoleModule' },
            { path: 'function', loadChildren: './function/function.module#FunctionModule' },
            { path: 'post-category', loadChildren: './post-category/post-category.module#PostCategoryModule' },
             { path: 'post', loadChildren: './post/post.module#PostModule' },
             { path: 'page', loadChildren: './page/page.module#PageModule' }
        ]
    }
];
@NgModule({
  imports: [RouterModule.forChild(adminRouting)],
  exports: [RouterModule]
})

export class AdminRoutingModule { }