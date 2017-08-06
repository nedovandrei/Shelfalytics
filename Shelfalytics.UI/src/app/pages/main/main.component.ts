import { Component, OnInit, AfterViewInit, Input, Output } from "@angular/core";
import { IDateRangePickerParams } from "app/shared/controls/daterangepicker/daterangepicker.model";
import { GlobalFilter } from "../../shared/services/global-filter.service";
import { MainService } from "./main.service";
import * as moment from "moment";

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"],
  providers: [MainService]
})
export class MainComponent implements OnInit, AfterViewInit {

  constructor(private mainService: MainService, private globalFilter: GlobalFilter) { }


  private mapInit: boolean = false;
  private chartsInit: boolean = false;
  private initFlag: boolean = false;


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

  private topSkuOosChart = {
    dataProvider: [],
    legendField: "SKUName",
    valueFields: ["OOSPercentage"]
  };
  private totalOOSPercentage: number;

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
    this.loadData();
    
  }

  private loadData() {
    this.chartsInit = false;

    this.mainService.getTopSkuOos().subscribe((result: any) => {
      this.topSkuOosChart.dataProvider = result.OOSProducts;
      this.totalOOSPercentage = result.TotalOOS.toFixed(2);
      this.chartsInit = true;
    });
  }

  ngAfterViewInit() {
    this.baCardTabChangeCallback = (index: number) => {
      console.log("ba card tab push, index", index);
      console.log("main component ba card", this.baCardTabs[index].title);
    };
    this.initFlag = true;
  }

}
