import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { routing } from "./pages.routing";
import { NgaModule } from "../theme/nga.module";
import { AppTranslationModule } from "../app.translation.module";

import { Pages } from "./pages.component";

import { MainComponent } from "./main/main.component";

@NgModule({
  imports: [CommonModule, AppTranslationModule, NgaModule, routing],
  declarations: [Pages, MainComponent],
  
})
export class PagesModule {
}
