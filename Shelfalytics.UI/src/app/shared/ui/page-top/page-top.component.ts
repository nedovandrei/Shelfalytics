import { Component, Input, Output, EventEmitter, OnInit } from "@angular/core";
import { AuthService } from "../../../auth/auth.service";
import { GlobalState } from "../../../global.state";
import { IDateRangePickerParams } from "../../controls/daterangepicker/daterangepicker.model";
import { TranslateService } from "@ngx-translate/core";
import { Language } from "../../../global";
import { GlobalFilter } from "../../../shared/services/global-filter.service";

@Component({
  selector: "page-top",
  templateUrl: "./page-top.component.html",
  styleUrls: ["./page-top.component.scss"]
})
export class PageTopComponent implements OnInit {

  @Input() daterangeParams: IDateRangePickerParams;
  @Output() onDateRangeChange = new EventEmitter();
  // @Output() onLanguageChanged = new EventEmitter();

  isScrolled: boolean = false;
  isMenuCollapsed: boolean = false;

  private currentLanguage = "en";

  private loggedUser: any;

  constructor(private _state: GlobalState, 
    private auth: AuthService, 
    private language: Language,
    private translate: TranslateService,
    private globalFilter: GlobalFilter
  ) {
    this._state.subscribe("menu.isCollapsed", (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
  }

  private showGlobalStateIndicator: boolean = false;
  private globalStateIndicatorValue: boolean = false;

  ngOnInit() {
    this.globalFilter.globalStateSubject.subscribe((state: boolean) => {
      // console.log("globalStateSubject, ", state);
      this.showGlobalStateIndicator = state;
      this.globalFilter.showGlobalState = state;
    });
    this.globalFilter.globalStateValueSubject.subscribe((value: boolean) => {
      // console.log("globalStateValueSubject, ", value);
      this.globalStateIndicatorValue = value;
      this.globalFilter.globalStateValue = value;
    });
    this.loggedUser = this.auth.getUserData();
    this.language.currentLanguage = this.currentLanguage = this.translate.currentLang;

    // console.log("PageTop userdata", this.loggedUser);
  }

  toggleMenu() {
    this.isMenuCollapsed = !this.isMenuCollapsed;
    this._state.notifyDataChanged("menu.isCollapsed", this.isMenuCollapsed);
    return false;
  }

  scrolledChanged(isScrolled) {
    this.isScrolled = isScrolled;
  }

  private logout() {
    this.auth.logout();
  }

  private dateChangedHandler(value: any) {
    // console.log("Date Changed, page-top", value);
    this.onDateRangeChange.emit(value);
  }

  private changeLanguage(lang: string) {
    this.currentLanguage = lang;
    this.language.onLanguageChange.next(lang);
  }
}
