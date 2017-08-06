import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgbDropdownModule, NgbModalModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule } from "@ngx-translate/core";

import { routing } from "./pages.routing";
import { AjaxService } from "../shared/services/ajax.service";
import { NgaModule } from "../theme/nga.module";
import { ControlsModule } from "../shared/controls/controls.module";
import { UiModule } from "../shared/ui/ui.module";
import { MapsModule } from "../shared/maps/maps.module";
import { ChartsModule } from "../shared/charts/charts.module";

import { Pages } from "./pages.component";

import { GlobalFilter } from "../shared/services/global-filter.service";

import { MainComponent } from "./main/main.component";
import { StatisticsComponent } from "./statistics/statistics.component";


@NgModule({
  imports: [
    CommonModule,
    TranslateModule,
    NgaModule,
    routing,
    NgbDropdownModule,
    NgbModalModule,
    ControlsModule,
    UiModule,
    MapsModule,
    ChartsModule
  ],
  declarations: [Pages, MainComponent, StatisticsComponent],
  providers: [GlobalFilter, AjaxService]

})
export class PagesModule {
}
