import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ControlsModule } from "../controls/controls.module";
import { NgaModule } from "../../theme/nga.module";

import { PageTopComponent } from "./page-top/page-top.component";

@NgModule({
    imports: [
        CommonModule,
        NgaModule,
        ControlsModule
    ],
    declarations: [PageTopComponent],
    exports: [PageTopComponent]
})
export class UiModule {

}
