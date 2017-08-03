import { Component, Input, Output, EventEmitter } from "@angular/core";

import { GlobalState } from "../../../global.state";
import { IDateRangePickerParams } from "../../controls/daterangepicker/daterangepicker.model";



@Component({
  selector: "page-top",
  templateUrl: "./page-top.component.html",
  styleUrls: ["./page-top.component.scss"]
})
export class PageTopComponent {

  @Input() daterangeParams: IDateRangePickerParams;
  @Output() onDateRangeChange = new EventEmitter();
  @Output() onLanguageChanged = new EventEmitter();

  isScrolled: boolean = false;
  isMenuCollapsed: boolean = false;

  private currentLanguage = "en";

  constructor(private _state: GlobalState) {
    this._state.subscribe("menu.isCollapsed", (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
  }

  toggleMenu() {
    this.isMenuCollapsed = !this.isMenuCollapsed;
    this._state.notifyDataChanged("menu.isCollapsed", this.isMenuCollapsed);
    return false;
  }

  scrolledChanged(isScrolled) {
    this.isScrolled = isScrolled;
  }

  private dateChangedHandler(value: any) {
    console.log("Date Changed, page-top", value);
    this.onDateRangeChange.emit(value);
  }

  private changeLanguage(lang: string) {
    console.log(lang);
    this.onLanguageChanged.emit(lang);
    
  }
}
