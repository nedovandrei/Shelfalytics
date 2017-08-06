import { Component, Input, Output, EventEmitter, ViewEncapsulation } from "@angular/core";
import { IDateRangePickerParams } from "../../controls/daterangepicker/daterangepicker.model";
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
  @Input() tabChangeCallback: (index: number) => void;
  @Output() onDateRangeChange = new EventEmitter();

  private selectedTab: number = 0;

  private daterangeParams: IDateRangePickerParams = {
    startDate: moment().subtract(1, "weeks"),
    endDate: moment(),
    isDateInputVisible: false
  };

  private dateChangedHandler(value: any) {
    console.log("Date Changed, page-top", value);
    this.onDateRangeChange.emit(value);
  }

  private onTabChange(index) {
    this.selectedTab = index;
    if (this.tabChangeCallback) {
      this.tabChangeCallback(index);
    }
  }
}
