import { Component, Input, Output, EventEmitter, ViewEncapsulation } from "@angular/core";
import { IDateRangePickerParams } from "../../controls/daterangepicker/daterangepicker.model";
import { TabDateRanges } from "./baCard.model";
import * as moment from "moment";

@Component({
  selector: "ad-ba-card",
  templateUrl: "./baCard.html",
  styleUrls: ["./baCard.scss"]
})
export class AdBaCardComponent {
  @Input() title: String;
  @Input() navTabs: String;
  @Input() baCardClass: String;
  @Input() cardType: String;
  // @Input() tabChangeCallback: (index: any) => void;
  @Output() onTabChange = new EventEmitter();
  // @Output() onDateRangeChange = new EventEmitter();

  private selectedTab: number = 0;
  private selectedCustomDateRange: any;

  private daterangeParams: IDateRangePickerParams = {
    startDate: moment().utc(true).subtract(1, "weeks"),
    endDate: moment().utc(true),
    isDateInputVisible: false
  };

  private dateChangedHandler(value: any) {
    console.log("Date Changed, page-top", value);
    this.selectedCustomDateRange = value;
    this.onTabChange.emit({
      timeSpan: {
        StartTime: this.selectedCustomDateRange.start,
        EndTime: this.selectedCustomDateRange.end
      },
      title: this.title,
      range: TabDateRanges.Custom
    });
  }

  private tabChanged(range: any, index: number) {
    this.selectedTab = index;
    if (range === TabDateRanges.Custom) {
      // this.onTabChange.emit({
      //   timeSpan: {
      //     StartTime: this.selectedCustomDateRange.start,
      //     EndTime: this.selectedCustomDateRange.end
      //   },
      //   title: this.title
      // });
    } else if (range === TabDateRanges.Day) {
      this.onTabChange.emit({
        timeSpan: {
          StartTime: moment().utc(true).subtract(1, "days"),
          EndTime: moment().utc(true)
        },
        title: this.title,
        range
      });
    } else if (range === TabDateRanges.Week) {
      this.onTabChange.emit({
        timeSpan: {
          StartTime: moment().utc(true).subtract(1, "weeks"),
          EndTime: moment().utc(true)
        },
        title: this.title,
        range
      });
    } else if (range === TabDateRanges.Month) {
      this.onTabChange.emit({
        timeSpan: {
          StartTime: moment().utc(true).subtract(1, "months"),
          EndTime: moment().utc(true)
        },
        title: this.title,
        range
      });
    } else if (range === TabDateRanges.Global) {
      this.onTabChange.emit({
        title: this.title,
        range
      });
    }
    // if (this.tabChangeCallback) {
    //   this.tabChangeCallback(index);
    // }
  }
}
