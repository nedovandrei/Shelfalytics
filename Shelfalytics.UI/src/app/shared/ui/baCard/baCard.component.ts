import { Component, Input, Output, EventEmitter } from "@angular/core";
import { IDateRangePickerParams } from "../../controls/daterangepicker/daterangepicker.model";
@Component({
  selector: "ba-card",
  templateUrl: "./baCard.html",
  styleUrls: ["./baCard.scss"]
})
export class BaCard {
  @Input() title: String;
  @Input() navTabs: String;
  @Input() baCardClass: String;
  @Input() cardType: String;
  @Input() tabChangeCallback: (index: number) => void;
  @Input() daterangeParams: IDateRangePickerParams;
  @Output() onDateRangeChange = new EventEmitter();
  private dateChangedHandler(value: any) {
    console.log("Date Changed, page-top", value);
    this.onDateRangeChange.emit(value);
  }

  private selectedTab: number = 0;

  private onTabChange(index) {
    this.selectedTab = index;
    if (this.tabChangeCallback) {
      this.tabChangeCallback(index);
    }
  }
}
