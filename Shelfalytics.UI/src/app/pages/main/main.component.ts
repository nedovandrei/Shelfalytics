import { Component, OnInit, AfterViewInit, Input, Output, OnDestroy } from "@angular/core";
import { IDateRangePickerParams } from "app/shared/controls/daterangepicker/daterangepicker.model";
import { GlobalFilter } from "../../shared/services/global-filter.service";
import { MainService } from "./main.service";
import { TabDateRanges } from "../../shared/ui/baCard/baCard.model";
import * as _ from "underscore";
// import * as moment from "moment";
import { IGoogleMapsData, IMarker } from "app/shared/maps/google-maps/google-maps.model";

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"],
  providers: [MainService]
})
export class MainComponent implements OnInit, AfterViewInit, OnDestroy {

  constructor(private mainService: MainService, private globalFilter: GlobalFilter) { }

  private cardsTabsGlobalState = {
    topSkuOos: true,
    topPosInOos: true,
    lossesDueToOos: true,
    topBestBusinessDevs: true,
    topPos: true,
    topSkuSales: true,
    topBestBusinessDevelopers: true,
    get globalFilterState() {
      if (this.topSkuOos &&
        this.topPosInOos &&
        this.lossesDueToOos &&
        this.topBestBusinessDevs &&
        this.topPos &&
        this.topSkuSales &&
        this.topBestBusinessDevelopers
      ) {
        return true;
      } else {
        return false;
      }
    }
  };

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
      title: "bacardButtons.global",
      type: "text",
      range: TabDateRanges.Global
    },
    {
      title: "bacardButtons.day",
      type: "text",
      range: TabDateRanges.Day
    },
    {
      title: "bacardButtons.week",
      type: "text",
      range: TabDateRanges.Week
    },
    {
      title: "bacardButtons.month",
      type: "text",
      range: TabDateRanges.Month
    },
    {
      type: "datepicker",
      range: TabDateRanges.Custom
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

  private lossesDueToOosChartInit: boolean = false;
  private lossesDueToOosSummary: number;
  private lossesDueToOosChart = {
    dataProvider: [],
    legendField: "ShortSKUName",
    valueFields: ["Losses"]
  };
  private lossesInUnits: number = 0;

  private topBestBusinessDevelopersInit: boolean = false;
  private topBestBusinessDevelopersChart = {
    dataProvider: [],
    legendField: "EmployeeName",
    valueFields: ["OosPercentage"]
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

  private tabChanged(event: any) {
    console.log("tab changed main component", event);

    if (event.title === "main.cards.topSkuOos") {
      this.reloadTopSkuOosCardData(event);
    } else if (event.title === "main.cards.topPosBeingInOos") {
      this.reloadTopPosBeingInOos(event);
    } else if (event.title === "main.cards.topSkuSales") {
      this.reloadSalesSummary(event);
    } else if (event.title === "main.cards.lossesDueToOos") {
      this.reloadLossesDueToOos(event);
    } else if (event.title === "main.cards.topBestBusinessDevs") {
      this.reloadTopBestBusinessDevelopers(event);
    }

    if (this.cardsTabsGlobalState.globalFilterState) {
      this.globalFilter.globalStateValueSubject.next(true);
    } else {
      this.globalFilter.globalStateValueSubject.next(false);
    }
  }

  //#region TabLogic
  private reloadTopSkuOosCardData(event: any) {
    this.skuOosChartsInit = false;

    if (event.range === TabDateRanges.Global) {
      this.cardsTabsGlobalState.topSkuOos = true;
      this.mainService.getTopSkuOos().subscribe((result: any) => {
        this.topSkuOosChart.dataProvider = result.OOSProducts;
        this.skuOosChartsInit = true;
      });
    } else {
      this.cardsTabsGlobalState.topSkuOos = false;
      this.mainService.getTopSkuOos(event.timeSpan).subscribe((result: any) => {
        this.topSkuOosChart.dataProvider = result.OOSProducts;
        this.skuOosChartsInit = true;
      });

    }
  }

  private reloadTopPosBeingInOos(event: any) {
    this.posInOosSummaryChartInit = false;

    if (event.range === TabDateRanges.Global) {
      this.cardsTabsGlobalState.topPosInOos = true;
      this.mainService.getTopPosInOos().subscribe((data: any) => {
        this.posInOosSummaryChart.dataProvider = data;
        this.posInOosSummaryChartInit = true;
      });
    } else {
      this.cardsTabsGlobalState.topPosInOos = false;
      this.mainService.getTopPosInOos(event.timeSpan).subscribe((data: any) => {
        this.posInOosSummaryChart.dataProvider = data;
        this.posInOosSummaryChartInit = true;
      });
    }

  }

  private reloadSalesSummary(event: any) {
    this.salesSummaryChartInit = false;

    if (event.range === TabDateRanges.Global) {
      this.cardsTabsGlobalState.topSkuSales = true;
      this.mainService.getSalesSummary().subscribe((result: any) => {
        this.salesSummaryChart.dataProvider = result.Products;
        this.salesSummaryChartInit = true;
      });
    } else {
      this.cardsTabsGlobalState.topSkuSales = false;
      this.mainService.getSalesSummary(event.timeSpan).subscribe((result: any) => {
        this.salesSummaryChart.dataProvider = result.Products;
        this.salesSummaryChartInit = true;
      });
    }

  }

  private reloadLossesDueToOos(event: any) {
    this.lossesDueToOosChartInit = false;

    if (event.range === TabDateRanges.Global) {
      this.cardsTabsGlobalState.lossesDueToOos = true;
      this.mainService.getLossesDueToOosSummary().subscribe((result: any) => {
        this.lossesDueToOosChart.dataProvider = result.LossesByProducts;
        this.lossesDueToOosChartInit = true;
      });
    } else {
      this.cardsTabsGlobalState.lossesDueToOos = false;
      this.mainService.getLossesDueToOosSummary(event.timeSpan).subscribe((result: any) => {
        this.lossesDueToOosChart.dataProvider = result.LossesByProducts;
        this.lossesDueToOosChartInit = true;
      });
    }
  }

  private reloadTopBestBusinessDevelopers(event: any) {
    this.topBestBusinessDevelopersInit = false;

    if (event.range === TabDateRanges.Global) {
      this.cardsTabsGlobalState.topBestBusinessDevelopers = true;
      this.mainService.getTopBestBusinessDevelopers().subscribe((result: any) => {
        this.topBestBusinessDevelopersChart.dataProvider = result;
        this.topBestBusinessDevelopersInit = true;
      });
    } else {
      this.cardsTabsGlobalState.topBestBusinessDevelopers = false;
      this.mainService.getTopBestBusinessDevelopers(event.timeSpan).subscribe((result: any) => {
        this.topBestBusinessDevelopersChart.dataProvider = result;
        this.topBestBusinessDevelopersInit = true;
      });
    }
  }
  //#endregion

  ngOnInit() {
    this.globalFilter.globalStateSubject.next(true);
    this.globalFilter.globalStateValueSubject.next(true);
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
    this.lossesDueToOosChartInit = false;
    this.topBestBusinessDevelopersInit = false;

    this.mainService.getTopSkuOos().subscribe((result: any) => {
      this.topSkuOosChart.dataProvider = _.map(result.OOSProducts, (item: any) => {
        item.OOSPercentage = item.OOSPercentage.toFixed(2);
        return item;
      });
      this.totalOOSPercentage = result.TotalOOS.toFixed(2);
      this.skuOosChartsInit = true;
    });

    this.mainService.getTopBestBusinessDevelopers().subscribe((result: any) => {
      this.topBestBusinessDevelopersChart.dataProvider = result;
      this.topBestBusinessDevelopersInit = true;
    });

    this.mainService.getSalesSummary().subscribe((result: any) => {
      this.salesSummaryCount = result.SalesCount;
      this.salesSummaryChart.dataProvider = result.Products;
      this.salesSummaryChartInit = true;
    });

    this.mainService.getLossesDueToOosSummary().subscribe((result: any) => {
      this.lossesInUnits = 0;
      this.lossesDueToOosSummary = result.Total;
      _.each(result.LossesByProducts, (item: any) => {
        this.lossesInUnits += item.AverageSalesPerOos;
      });
      this.lossesDueToOosChart.dataProvider = result.LossesByProducts;
      this.lossesDueToOosChartInit = true;
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
    // this.baCardTabChangeCallback = (index: number) => {
    //   console.log("ba card tab push, index", index);
    //   console.log("main component ba card", this.baCardTabs[index].title);
    // };
    this.loadData();
    this.initFlag = true;
  }

  ngOnDestroy() {
    this.globalFilter.globalStateSubject.next(false);
  }

}
