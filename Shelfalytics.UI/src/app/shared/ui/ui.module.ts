import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { ControlsModule } from "../controls/controls.module";
import { NgaModule } from "../../theme/nga.module";
import { TranslateModule } from "@ngx-translate/core";

import { PageTopComponent } from "./page-top/page-top.component";

@NgModule({
    imports: [
        CommonModule,
        NgaModule,
        ControlsModule,
        FormsModule
    ],
    declarations: [PageTopComponent],
    exports: [PageTopComponent]
})
export class UiModule {

}
