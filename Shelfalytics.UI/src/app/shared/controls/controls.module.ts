import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgaModule } from '../../theme/nga.module';
import {NgGridModule, NgGridItem, NgGridConfig, NgGridItemConfig, NgGridItemEvent} from 'angular2-grid';
import { GridstackComponent } from './gridstack/gridstack.component';
import { SmartTableComponent } from './smart-table/smart-table.component';
import { AngularGridComponent } from './angular2-grid/angular2-grid.component';

@NgModule({
  imports: [
    CommonModule,
    Ng2SmartTableModule,
    NgaModule,
    NgGridModule
  ],
  declarations: [GridstackComponent, SmartTableComponent, AngularGridComponent],
  exports: [GridstackComponent, SmartTableComponent, AngularGridComponent],
})
export class ControlsModule { }
