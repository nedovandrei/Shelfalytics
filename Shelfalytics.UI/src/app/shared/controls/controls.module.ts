import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgaModule } from '../../theme/nga.module';

import { GridstackComponent } from './gridstack/gridstack.component';
import { SmartTableComponent } from './smart-table/smart-table.component';


@NgModule({
  imports: [
    CommonModule,
    Ng2SmartTableModule,
    NgaModule,
  ],
  declarations: [GridstackComponent, SmartTableComponent],
  exports: [GridstackComponent, SmartTableComponent],
})
export class ControlsModule { }
