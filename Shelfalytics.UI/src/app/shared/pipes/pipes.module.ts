import { NgModule } from "@angular/core";
import { TimespanPipe } from "./timespan/timespan.pipe";
import { FahrenheitPipe } from "./fahrenheit/fahrenheit.pipe";
 
@NgModule({
    imports: [],
    exports: [TimespanPipe, FahrenheitPipe],
    declarations: [TimespanPipe, FahrenheitPipe]
})
export class PipesModule {

}
