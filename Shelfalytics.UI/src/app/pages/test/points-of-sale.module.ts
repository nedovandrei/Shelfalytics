import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routing } from './points-of-sale.routing';
import { PointsOfSaleComponent } from './points-of-sale.component';

import { NgaModule } from '../../theme/nga.module';
import { ControlsModule } from '../../shared/controls/controls.module';
import { ChartsModule } from '../../shared/charts/charts.module';

import { AjaxService } from '../../shared/services/ajax.service';
import { PosInfoComponent } from './pos-info/pos-info.component';
import { EquipmentInfoComponent } from './pos-info/equipment-info/equipment-info.component';
import { PosListComponent } from './pos-list/pos-list.component';

@NgModule({
  imports: [
    routing,
    NgaModule,
    CommonModule,
    ChartsModule,
    ControlsModule,
  ],
  declarations: [PointsOfSaleComponent, PosInfoComponent, EquipmentInfoComponent, PosListComponent],
  providers: [AjaxService]
})
export class PointsOfSaleModule { }
