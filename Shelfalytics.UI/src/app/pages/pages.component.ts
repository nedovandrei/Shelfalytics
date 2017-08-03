import { Component, OnInit } from "@angular/core";
import { Routes } from "@angular/router";

import { BaMenuService } from "../theme";
import { GlobalFilter } from "../shared/services/global-filter.service";
import { IDateRangePickerParams } from "../shared/controls/daterangepicker/daterangepicker.model";
import { TranslateService } from "@ngx-translate/core";

import { PAGES_MENU } from "./pages.menu";

import * as moment from "moment";

@Component({
  selector: "pages",
  templateUrl: "pages.component.html" 
})
export class Pages implements OnInit {

  constructor(private _menuService: BaMenuService, 
    private globalFilter: GlobalFilter, 
    private translate: TranslateService
  ) { }

  private dateRangePickerParams: IDateRangePickerParams = {
    startDate: moment().subtract(1, "weeks"),
    endDate: moment()
  };

  ngOnInit() {
    this._menuService.updateMenuByRoutes(<Routes>PAGES_MENU);
  }

  private dateRangeChanged(value: any) {
    console.log("date range changed, pages.comp", value);

    this.globalFilter.startDate = value.start;
    this.globalFilter.endDate = value.end;
    console.log("global filter", this.globalFilter);
    this.globalFilter.onDateRangeChanged.next(value);
  }

  private languageChange(lang: string) {
    this.translate.use(lang);
  }
}
