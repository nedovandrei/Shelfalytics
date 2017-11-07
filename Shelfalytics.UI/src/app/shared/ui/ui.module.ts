import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { ControlsModule } from "../controls/controls.module";
import { NgaModule } from "../../theme/nga.module";
import { TranslateModule } from "@ngx-translate/core";

import { PageTopComponent } from "./page-top/page-top.component";
import { AdBaCardComponent } from "./baCard/baCard.component";
@NgModule({
    imports: [
        CommonModule,
        NgaModule,
        ControlsModule,
        FormsModule,
        TranslateModule.forChild()
    ],
    declarations: [PageTopComponent, AdBaCardComponent],
    exports: [PageTopComponent, AdBaCardComponent]
})
export class UiModule {

}
