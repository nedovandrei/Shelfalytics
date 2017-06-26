import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridstackComponent } from './gridstack/gridstack.component';


@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [GridstackComponent],
  exports: [GridstackComponent]
})
export class ControlsModule { }
