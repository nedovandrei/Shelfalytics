import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { DaterangepickerConfig } from "ng2-daterangepicker";
import { IDateRangePickerParams } from "./daterangepicker.model";
import * as moment from "moment";

@Component({
  selector: "daterangepicker",
  templateUrl: "./daterangepicker.component.html",
  styleUrls: ["./daterangepicker.component.scss"]
})
export class DaterangepickerComponent implements OnInit {

  constructor() {
    // this.daterangepickerOptions.settings = {
    //   locale: { format: "DD-MM-YYYY" },
    //   alwaysShowCalendars: true,
    //   ranges: {
    //     "Last Week": [moment().subtract(1, "weeks"), moment()],
    //     "Last Month": [moment().subtract(1, "month"), moment()],
    //     "Last 6 Months": [moment().subtract(6, "month"), moment()],
    //     "Last Year": [moment().subtract(12, "month"), moment()]
    //   }
    // };
  }
  private datesInitialized: boolean = false;

  private daterangepickerOptions = {
      showSelectorArrow: false,
      locale: { format: "DD.MM.YYYY" },
      alwaysShowCalendars: true,
      ranges: {
        "Last Week": [moment().utc(true).subtract(1, "weeks"), moment()],
        "Last Month": [moment().utc(true).subtract(1, "month"), moment()],
        "Last 6 Months": [moment().utc(true).subtract(6, "month"), moment()],
        "Last Year": [moment().utc(true).subtract(12, "month"), moment()]
      },
      startDate: undefined,
      endDate: undefined
  };

  private isDateInputsVisible: boolean = true;

  private selectedDate: IDateRangePickerParams = undefined;

  @Input() params: IDateRangePickerParams;
  @Output() dateChanged = new EventEmitter();

  ngOnInit() {

    this.selectedDate = {
      startDate: this.params.startDate,
      endDate: this.params.endDate
    };

    this.daterangepickerOptions.startDate = this.params.startDate;
    this.daterangepickerOptions.endDate = this.params.endDate;

    if (this.params.isDateInputVisible !== undefined) {
      this.isDateInputsVisible = this.params.isDateInputVisible;
    }
    this.datesInitialized = true;
  }



  // private eventLog: any;

  private onDateSelect(value: any) {
    // console.log("selected date ", value);
    this.selectedDate.startDate = value.start;
    this.selectedDate.endDate = value.end;
    this.dateChanged.emit(value);
  }

  // private applyDate(value: any, dateInput: any) {
  //   console.log("apply date", value);
  //     // dateInput.start = value.start;
  //     // dateInput.end = value.end;
  // }

  // calendarEventsHandler(e: any) {
  //     console.log(e);
  //     this.eventLog += '\nEvent Fired: ' + e.event.type;
  // }
}
