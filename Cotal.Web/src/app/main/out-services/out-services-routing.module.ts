import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OutServicesComponent } from "app/main/out-services/out-services.component";

const routes: Routes = [
   {
    path: '', component: OutServicesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OutServicesRoutingModule { }
