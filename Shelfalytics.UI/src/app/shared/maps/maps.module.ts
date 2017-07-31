import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AgmCoreModule } from "@agm/core";
import { GoogleMapsComponent } from "./google-maps/google-maps.component";
import { RouterModule } from "@angular/router";
import { global } from "../../global";

@NgModule({
  imports: [
    CommonModule,
    AgmCoreModule.forRoot({
      apiKey: global.googleMapsApiKey
    }),
    RouterModule
  ],
  declarations: [GoogleMapsComponent],
  exports: [GoogleMapsComponent]
})
export class MapsModule { }
