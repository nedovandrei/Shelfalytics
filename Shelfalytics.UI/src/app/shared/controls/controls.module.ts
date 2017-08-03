import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule, NG_VALUE_ACCESSOR, ControlValueAccessor } from "@angular/forms";
import { Ng2SmartTableModule } from "ng2-smart-table";
import { NgaModule } from "../../theme/nga.module";
import { NgGridModule, NgGridItem, NgGridConfig, NgGridItemConfig, NgGridItemEvent } from "angular2-grid";
import { NgbModalModule } from "@ng-bootstrap/ng-bootstrap";
import { RouterModule } from "@angular/router";
import { SlimScrollModule } from "ng2-slimscroll";


import { TranslateModule } from "@ngx-translate/core";
import { SlimScrollOptions } from "ng2-slimscroll";
import { Daterangepicker } from "ng2-daterangepicker";
import { ModalModule } from "angular2-modal";
import { BootstrapModalModule } from "angular2-modal/plugins/bootstrap";
import { Overlay } from "angular2-modal";
import { Modal } from "angular2-modal/plugins/bootstrap";

import { GridstackComponent } from "./gridstack/gridstack.component";
import { SmartTableComponent } from "./smart-table/smart-table.component";
import { AngularGridComponent } from "./angular2-grid/angular2-grid.component";
import { ADTableComponent } from "./adtable/adtable.component";
import { DefaultModalComponent } from "./default-modal/default-modal.component";
import { DatePickerComponent } from "./ng2-datepicker/ng2-datepicker.component";
import { DaterangepickerComponent } from "./daterangepicker/daterangepicker.component";
import { Angular2ModalComponent } from "./angular2-modal/angular2-modal.component";


@NgModule({
  imports: [
    CommonModule,
    Ng2SmartTableModule,
    TranslateModule,
    NgaModule,
    NgGridModule,
    NgbModalModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    SlimScrollModule,
    Daterangepicker,
    ModalModule,
    BootstrapModalModule
  ],
  declarations: [
    GridstackComponent,
    SmartTableComponent,
    AngularGridComponent,
    DefaultModalComponent,
    ADTableComponent,
    DatePickerComponent,
    DaterangepickerComponent,
    Angular2ModalComponent
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
    DaterangepickerComponent,
    Angular2ModalComponent
  ],
})
export class ControlsModule { }
