import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AgmCoreModule } from "@agm/core";
import { GoogleMapsComponent } from "./google-maps/google-maps.component";
import { DynamicGoogleMapsComponent } from "./dynamic-google-maps/dynamic-google-maps.component";
import { RouterModule } from "@angular/router";
import {BrowserModule} from '@angular/platform-browser';
import { global } from "../../global";

@NgModule({
  imports: [
    CommonModule,
    AgmCoreModule.forRoot({
      apiKey: global.googleMapsApiKey
    }),
    RouterModule,
    BrowserModule,
  ],
  declarations: [GoogleMapsComponent, DynamicGoogleMapsComponent],
  exports: [GoogleMapsComponent, DynamicGoogleMapsComponent]
})
export class MapsModule { }
