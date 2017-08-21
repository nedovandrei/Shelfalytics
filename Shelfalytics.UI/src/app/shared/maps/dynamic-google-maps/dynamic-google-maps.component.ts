import {Component,NgModule} from '@angular/core';
// import { marker } from "./dynamic-google-maps.model";
@Component({
  selector: "dynamic-google-maps",
  templateUrl: "./dynamic-google-maps.component.html",
  styleUrls: ["./dynamic-google-maps.component.scss"]
})



export class DynamicGoogleMapsComponent {
  title: string = 'POS Addres';
    lat: number = 51.678418;
    lng: number = 7.809007;
  }
