import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgbDropdownModule, NgbModalModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule } from "@ngx-translate/core";

import { routing } from "./pages.routing";
import { NgaModule } from "../theme/nga.module";
import { ControlsModule } from "../shared/controls/controls.module";
import { UiModule } from "../shared/ui/ui.module";

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
    UiModule
  ],
  declarations: [Pages, MainComponent, StatisticsComponent],
  providers: [GlobalFilter]

})
export class PagesModule {
}
