import { Component, OnInit } from "@angular/core";
import { DaterangepickerConfig } from "ng2-daterangepicker";
import * as moment from "moment";

@Component({
  selector: "daterangepicker",
  templateUrl: "./daterangepicker.component.html",
  styleUrls: ["./daterangepicker.component.scss"]
})
export class DaterangepickerComponent implements OnInit {

  constructor(private daterangepickerOptions: DaterangepickerConfig) {
    this.daterangepickerOptions.settings = {
      locale: { format: "DD-MM-YYYY" },
      alwaysShowCalendars: true,
      ranges: {
        "Last Week": [moment().subtract(1, "weeks"), moment()],
        "Last Month": [moment().subtract(1, "month"), moment()],
        "Last 6 Months": [moment().subtract(6, "month"), moment()],
        "Last Year": [moment().subtract(12, "month"), moment()]
      }
    };
  }

  ngOnInit() {
  }

  private defaultDate: any = {
    start: moment(),
    end: moment().add(5, "month")
  };

  private eventLog: any;

  private selectedDate(value: any, dateInput: any) {
      dateInput.start = value.start;
      dateInput.end = value.end;
  }

  private applyDate(value: any, dateInput: any) {
      dateInput.start = value.start;
      dateInput.end = value.end;
  }

  calendarEventsHandler(e: any) {
      console.log(e);
      this.eventLog += '\nEvent Fired: ' + e.event.type;
  }
}
