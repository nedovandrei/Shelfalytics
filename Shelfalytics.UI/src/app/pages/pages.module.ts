import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';

import { routing }       from './pages.routing';
import { NgaModule } from '../theme/nga.module';
import { AppTranslationModule } from '../app.translation.module';
import { TestComponent } from "./test/test.component";
import { ChartsModule } from "../shared/charts/charts.module";

import { AjaxService } from "../shared/services/ajax.service";


import { Pages } from './pages.component';
import { EquipmentInfoComponent } from './test/equipment-info/equipment-info.component';

@NgModule({
  imports: [CommonModule, AppTranslationModule, NgaModule, routing, ChartsModule],
  declarations: [Pages, TestComponent, EquipmentInfoComponent],
  providers: [AjaxService]
})
export class PagesModule {
}
