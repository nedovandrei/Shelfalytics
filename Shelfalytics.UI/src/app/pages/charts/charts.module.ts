import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { NgaModule } from "../../theme/nga.module";

import { routing } from "./charts.routing";
import { Charts } from "./charts.component";
import { ChartistJs } from "./components/chartistJs/chartistJs.component";
import { ChartistJsService } from "./components/chartistJs/chartistJs.service";
import { TranslateModule } from "@ngx-translate/core";

@NgModule({
  imports: [
    CommonModule,
    TranslateModule.forChild(),
    FormsModule,
    NgaModule,
    routing
  ],
  declarations: [
    Charts,
    ChartistJs
  ],
  providers: [
    ChartistJsService
  ]
})
export class ChartsModule {}
