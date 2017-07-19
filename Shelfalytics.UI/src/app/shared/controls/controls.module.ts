import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgaModule } from '../../theme/nga.module';
import { NgGridModule, NgGridItem, NgGridConfig, NgGridItemConfig, NgGridItemEvent} from 'angular2-grid';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

import { GridstackComponent } from './gridstack/gridstack.component';
import { SmartTableComponent } from './smart-table/smart-table.component';
import { AngularGridComponent } from './angular2-grid/angular2-grid.component';
import { DefaultModalComponent } from './default-modal/default-modal.component';

@NgModule({
  imports: [
    CommonModule,
    Ng2SmartTableModule,
    NgaModule,
    NgGridModule,
    NgbModalModule
  ],
  declarations: [GridstackComponent, SmartTableComponent, AngularGridComponent, DefaultModalComponent],
  exports: [GridstackComponent, SmartTableComponent, AngularGridComponent, DefaultModalComponent],
})
export class ControlsModule { }
