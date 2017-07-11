import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from "app/main/main.component";

const routes: Routes = [
  {
    path: '', component: MainComponent, children: [ 
      { path: 'about', loadChildren: "./aboutus/aboutus.module#AboutusModule" },
      { path: 'contact', loadChildren: "./contact/contact.module#ContactModule" },
      { path: 'news', loadChildren: "./news/news.module#NewsModule" },
      { path: 'out-services', loadChildren: "./out-services/out-services.module#OutServicesModule" }
    ]
  },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
