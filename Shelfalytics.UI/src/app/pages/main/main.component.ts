import { Component, OnInit } from '@angular/core';
import { IGoogleMapsData, ICoordinates, IMarker } from "../../shared/maps/google-maps/google-maps.model";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  private mapSettings: IGoogleMapsData = {
    center: {
      lat: 0,
      lng: 0
    },
    // zoom: 16,
    markers: [],
    zoomControl: true
  };
  constructor() { }

  ngOnInit() {
  }

}
