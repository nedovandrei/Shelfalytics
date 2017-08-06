import { Component, OnInit, AfterViewInit, Input, Output } from "@angular/core";
import { IDateRangePickerParams } from "app/shared/controls/daterangepicker/daterangepicker.model";
import * as moment from "moment";

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"]
})
export class MainComponent implements OnInit, AfterViewInit {

  constructor() { }


  private mapInit: boolean = false;
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
    dataProvider: [
      {
        SKUName: "Fourchette",
        OOSPercentage: 43
      },
      {
        SKUName: "Nr1",
        OOSPercentage: 24
      },
      {
        SKUName: "Green Hills",
        OOSPercentage: 33
      }
    ],
    legendField: "SKUName",
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

  }

  ngAfterViewInit() {
    this.baCardTabChangeCallback = (index: number) => {
      console.log("ba card tab push, index", index);
      console.log("main component ba card", this.baCardTabs[index].title);
    };
    this.initFlag = true;
  }

}
