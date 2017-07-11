import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OutServicesRoutingModule } from './out-services-routing.module';
import { OutServicesComponent } from './out-services.component';

@NgModule({
  imports: [
    CommonModule,
    OutServicesRoutingModule
  ],
  declarations: [OutServicesComponent]
})
export class OutServicesModule { }
