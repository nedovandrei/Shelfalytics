import { Component, OnInit, AfterViewInit, Input, Output } from "@angular/core";
import { IDateRangePickerParams } from "app/shared/controls/daterangepicker/daterangepicker.model";
import { GlobalFilter } from "../../shared/services/global-filter.service";
import { MainService } from "./main.service";
import * as _ from "underscore";
import * as moment from "moment";
import { IGoogleMapsData, IMarker } from "app/shared/maps/google-maps/google-maps.model";

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"],
  providers: [MainService]
})
export class MainComponent implements OnInit, AfterViewInit {

  constructor(private mainService: MainService, private globalFilter: GlobalFilter) { }


  private mapInit: boolean = false;
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


  private baCardTabs: any[] = [
    {
      title: "Day"
    },
    {
      title: "Week"
    },
    {
      title: "Month"
    }
  ];

  private skuOosChartsInit: boolean = false;
  private topSkuOosChart = {
    dataProvider: [],
    legendField: "ShortSKUName",
    valueFields: ["OOSPercentage"]
  };
  private totalOOSPercentage: number;

  private salesSummaryChartInit: boolean = false;
  private salesSummaryCount: number;
  private salesSummaryChart = {
    dataProvider: [],
    legendField: "ShortProductName",
    valueFields: ["Sales"]
  };

  private posInOosSummaryChartInit: boolean = false;
  private posInOosSummaryChart = {
    dataProvider: [],
    legendField: "PointOfSaleName",
    valueFields: ["OOSPercentage"]
  };

  // private daterangeParams: IDateRangePickerParams = {
  //   startDate: moment().subtract(1, "weeks"),
  //   endDate: moment(),
  //   isDateInputVisible: false
  // };

  // private dateChangedHandler(value: any) {
  //   console.log("Date Changed, page-top", value);
  //   // this.onDateRangeChange.emit(value);
  // }

  private baCardTabChangeCallback: any;

  ngOnInit() {
    this.globalFilter.onDateRangeChanged.subscribe((dateTimeValue: any) => {
      console.log("fired onDateRangeChange event");
      this.loadData();
    });
    // this.loadData();
    
  }

  private loadData() {
    this.skuOosChartsInit = false;
    this.salesSummaryChartInit = false;
    this.posInOosSummaryChartInit = false;

    this.mainService.getTopSkuOos().subscribe((result: any) => {
      this.topSkuOosChart.dataProvider = result.OOSProducts;
      this.totalOOSPercentage = result.TotalOOS.toFixed(2);
      this.skuOosChartsInit = true;
    });

    this.mainService.getSalesSummary().subscribe((result: any) => {
      this.salesSummaryCount = result.SalesCount;
      this.salesSummaryChart.dataProvider = result.Products;
      this.salesSummaryChartInit = true;
    });

    this.mainService.getTopPosInOos().subscribe((data: any) => {
      this.posInOosSummaryChart.dataProvider = data;
      this.posInOosSummaryChartInit = true;

      for (let i = 0; i < data.length; i++) {

        const marker: IMarker = {
          coordinates: {
            lat: data[i].Latitude,
            lng: data[i].Longitude
          },
          // iconUrl: "http://maps.google.com/mapfiles/ms/icons/green-dot.png",
          title: data[i].PointOfSaleName,
          markerDraggable: false,
          label: data[i].PointOfSaleName.slice(0, 1),
          opacity: data[i].OOSPercentage === 0 ? 0.5 : 1,
          visible: true,
          infoWindowContent: `${data[i].PointOfSaleName}, ${data[i].PointOfSaleAddress}, OOS: ${data[i].OOSPercentage}%`,
          linkUrl: "/pages/points-of-sale/pos-info",
          linkUrlId: data[i].PointOfSaleId
        };

        this.mapSettings.markers.push(marker);
      }

      this.mapSettings.center.lat = _.reduce(this.mapSettings.markers, (memo: number, item: any) => {
        return memo + item.coordinates.lat;
      }, 0) / (this.mapSettings.markers.length === 0 ? 1 : this.mapSettings.markers.length);

      this.mapSettings.center.lng = _.reduce(this.mapSettings.markers, (memo: number, item: any) => {
        return memo + item.coordinates.lng;
      }, 0) / (this.mapSettings.markers.length === 0 ? 1 : this.mapSettings.markers.length);

      console.log("mapSettings", this.mapSettings);

      this.mapInit = true;
    });

  }

  ngAfterViewInit() {
    this.baCardTabChangeCallback = (index: number) => {
      console.log("ba card tab push, index", index);
      console.log("main component ba card", this.baCardTabs[index].title);
    };
    this.loadData();
    this.initFlag = true;
  }

}
