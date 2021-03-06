import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgbDropdownModule, NgbModalModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule } from "@ngx-translate/core";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { AgmCoreModule } from "@agm/core";
  import { BrowserModule } from "@angular/platform-browser";
import { routing } from "./pages.routing";
import { AuthGuard } from "../auth/auth.guard";
import { AjaxService } from "../shared/services/ajax.service";
import { NgaModule } from "../theme/nga.module";
import { ControlsModule } from "../shared/controls/controls.module";
import { UiModule } from "../shared/ui/ui.module";
import { MapsModule } from "../shared/maps/maps.module";
import { ChartsModule } from "../shared/charts/charts.module";
import { AnimateOnScrollModule } from 'ng2-animate-on-scroll';

import { Pages } from "./pages.component";

import { GlobalFilter } from "../shared/services/global-filter.service";

import { MainComponent } from "./main/main.component";
import { StatisticsComponent } from "./statistics/statistics.component";
import { SettingsComponent } from "./settings/settings.component";
// import { ProfileComponent } from "./profile/profile.component";

@NgModule({
  imports: [
    CommonModule,
    TranslateModule.forChild(),
    NgaModule,
    routing,
    NgbDropdownModule,
    NgbModalModule,
    ControlsModule,
    UiModule,
    MapsModule,
    ChartsModule,
    ReactiveFormsModule,
    FormsModule,
    AgmCoreModule,
    BrowserModule,
      AnimateOnScrollModule.forRoot()
  ],
  declarations: [Pages, MainComponent, StatisticsComponent, SettingsComponent],
  providers: [GlobalFilter, AjaxService, AuthGuard]

})
export class PagesModule {
}
