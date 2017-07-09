import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimpleTinyComponent } from "./simple-tiny.component";

@NgModule({
  imports: [
    CommonModule
  ],
  exports:[SimpleTinyComponent],
  declarations: [SimpleTinyComponent]
})
export class SimpleTinyModule { 
 // static forRoot(): ModuleWithProviders;
}
