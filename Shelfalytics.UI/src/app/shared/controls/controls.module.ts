import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, NG_VALUE_ACCESSOR, ControlValueAccessor} from "@angular/forms";
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { NgaModule } from '../../theme/nga.module';
import { NgGridModule, NgGridItem, NgGridConfig, NgGridItemConfig, NgGridItemEvent} from 'angular2-grid';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from "@angular/router";
import { SlimScrollModule } from 'ng2-slimscroll';
import { SlimScrollOptions } from 'ng2-slimscroll';
import { Daterangepicker } from "ng2-daterangepicker";

import { GridstackComponent } from './gridstack/gridstack.component';
import { SmartTableComponent } from './smart-table/smart-table.component';
import { AngularGridComponent } from './angular2-grid/angular2-grid.component';
import { ADTableComponent } from "./adtable/adtable.component";
import { DefaultModalComponent } from './default-modal/default-modal.component';
import { DatePickerComponent } from './ng2-datepicker/ng2-datepicker.component';
import { DaterangepickerComponent } from './daterangepicker/daterangepicker.component';

@NgModule({
  imports: [
    CommonModule,
    Ng2SmartTableModule,
    NgaModule,
    NgGridModule,
    NgbModalModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    SlimScrollModule,
    Daterangepicker
  ],
  declarations: [
    GridstackComponent,
    SmartTableComponent,
    AngularGridComponent,
    DefaultModalComponent,
    ADTableComponent,
    DatePickerComponent,
    DaterangepickerComponent
  ],
  exports: [
    GridstackComponent, 
    SmartTableComponent, 
    AngularGridComponent, 
    DefaultModalComponent, 
    ADTableComponent, 
    DatePickerComponent, 
    SlimScrollModule, 
    FormsModule,
    DaterangepickerComponent
  ],
})
export class ControlsModule { }
