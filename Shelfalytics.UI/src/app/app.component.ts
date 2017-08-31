import { Component, ViewContainerRef, AfterViewInit, OnInit } from "@angular/core";
import * as $ from "jquery";

import { GlobalState } from "./global.state";
import { BaImageLoaderService, BaThemePreloader, BaThemeSpinner } from "./theme/services";
import { BaThemeConfig } from "./theme/theme.config";
import { layoutPaths } from "./theme/theme.constants";
import { TranslateService } from "@ngx-translate/core";
import { Language } from "./global";
import { JwtHelper } from "angular2-jwt";
import { GlobalFilter } from "./shared/services/global-filter.service";

/*
 * App Component
 * Top Level Component
 */
@Component({
  selector: "app",
  styleUrls: ["./app.component.scss"],
  template: `
    <main [class.menu-collapsed]="isMenuCollapsed" baThemeRun>
      <div class="additional-bg"></div>
      <router-outlet></router-outlet>
    </main>
  `,
  providers: [JwtHelper]
})
export class App implements AfterViewInit, OnInit {

  isMenuCollapsed: boolean = false;

  constructor(private _state: GlobalState,
              private _imageLoader: BaImageLoaderService,
              private _spinner: BaThemeSpinner,
              private viewContainerRef: ViewContainerRef,
              private themeConfig: BaThemeConfig,
              private lang: Language,
              private translate: TranslateService,
              private jwt: JwtHelper,
              private filter: GlobalFilter) {

    themeConfig.config();

    this._loadImages();

    this._state.subscribe("menu.isCollapsed", (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
  }

  private setClientId() {
    if (!this.jwt.isTokenExpired(localStorage.getItem("token"))) {
      this.filter.clientId = this.jwt.decodeToken(localStorage.getItem("token")).clientId;
    }
  }
  // jwtHelper: JwtHelper = new JwtHelper();
  ngOnInit() {
    this.setClientId();

    this.filter.userLogged.subscribe(() => {
      this.setClientId();
    });
    

    this.lang.onLanguageChange.subscribe((language: string) => {
      this.translate.use(language);
      this.lang.currentLanguage = language;
    });
  }

  ngAfterViewInit(): void {
    // hide spinner once all loaders are completed
    BaThemePreloader.load().then((values) => {
      this._spinner.hide();
    });

    // this.useJwtHelper();
  }

//   useJwtHelper() {
//   const token = localStorage.getItem("token");

//   console.log(
//     this.jwtHelper.decodeToken(token),
//     this.jwtHelper.getTokenExpirationDate(token),
//     this.jwtHelper.isTokenExpired(token)
//   );
//   console.log(this.jwtHelper.urlBase64Decode);
// }

  private _loadImages(): void {
    // register some loaders
    BaThemePreloader.registerLoader(this._imageLoader.load("/assets/img/sky-bg.jpg"));
  }

}
