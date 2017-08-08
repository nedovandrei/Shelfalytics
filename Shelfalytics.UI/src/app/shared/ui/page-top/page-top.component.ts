import { Component, Input, Output, EventEmitter, OnInit } from "@angular/core";
import { AuthService } from "../../../auth/auth.service";
import { GlobalState } from "../../../global.state";
import { IDateRangePickerParams } from "../../controls/daterangepicker/daterangepicker.model";

@Component({
  selector: "page-top",
  templateUrl: "./page-top.component.html",
  styleUrls: ["./page-top.component.scss"]
})
export class PageTopComponent implements OnInit {

  @Input() daterangeParams: IDateRangePickerParams;
  @Output() onDateRangeChange = new EventEmitter();
  @Output() onLanguageChanged = new EventEmitter();

  isScrolled: boolean = false;
  isMenuCollapsed: boolean = false;

  private currentLanguage = "en";

  private loggedUser: any;

  constructor(private _state: GlobalState, private auth: AuthService) {
    this._state.subscribe("menu.isCollapsed", (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
  }

  ngOnInit() {
    this.loggedUser = this.auth.getUserData();

    console.log("PageTop userdata", this.loggedUser);
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
    console.log("Date Changed, page-top", value);
    this.onDateRangeChange.emit(value);
  }

  private changeLanguage(lang: string) {
    console.log(lang);
    this.onLanguageChanged.emit(lang);

  }
}
