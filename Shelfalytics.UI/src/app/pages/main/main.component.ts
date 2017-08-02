import { Component, OnInit, AfterViewInit } from "@angular/core";
import { IGoogleMapsData, ICoordinates, IMarker } from "../../shared/maps/google-maps/google-maps.model";

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"]
})
export class MainComponent implements OnInit, AfterViewInit {
  private mapSettings: IGoogleMapsData = {
    center: {
      lat: 0,
      lng: 0
    },
    // zoom: 16,
    markers: [],
    zoomControl: true
  };

  private initFlag: boolean = false;
  constructor() { }

  private baCardTabs: any[] = [
    {
      title: "Daily"
    },
    {
      title: "Monthly"
    },
    {
      title: "Yearly"
    }
  ];

  private baCardTabChangeCallback: any;

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.baCardTabChangeCallback = (index: number) => {
      console.log("ba card tab push, index", index);
      console.log("main component ba card", this.baCardTabs[index].title);
    };
    this.initFlag = true;
  }

}
