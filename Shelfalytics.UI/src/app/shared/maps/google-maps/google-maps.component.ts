import { Component, OnInit, Input, Output } from "@angular/core";
import { IGoogleMapsData, IMarker, ICoordinates } from "./google-maps.model";

class GoogleMapsData implements IGoogleMapsData {
    center: ICoordinates;
    zoom?: number = 11;
    markers: IMarker[];
    disableDefaultUI?: boolean = true;
    zoomControl?: boolean = true;
    clickableIcons?: boolean = true;
    draggableCursor?: string = ""; // [draggableCursor]="url(<some address>), pointer"
}

@Component({
  selector: "google-maps",
  templateUrl: "./google-maps.component.html",
  styleUrls: ["./google-maps.component.scss"]
})
export class GoogleMapsComponent implements OnInit {

  constructor() { }

  @Input() mapData: IGoogleMapsData;
  private targetMapData: IGoogleMapsData = undefined;
  private mapInit: boolean = false;

  ngOnInit() {
    this.targetMapData = new GoogleMapsData();

    Object.assign(this.targetMapData, this.mapData);
    this.mapInit = true;
  }
  private markerClicked(label: string, index: any) {
    console.log("maps marker clicked", label, index);
  }

  private mapClicked($event: MouseEvent) {
    console.log("map clicked", $event);
  }

}
