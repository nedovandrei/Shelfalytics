import { Component, ViewContainerRef, AfterViewInit, OnInit } from "@angular/core";
import * as $ from "jquery";

import { GlobalState } from "./global.state";
import { BaImageLoaderService, BaThemePreloader, BaThemeSpinner } from "./theme/services";
import { BaThemeConfig } from "./theme/theme.config";
import { layoutPaths } from "./theme/theme.constants";
import { TranslateService } from "@ngx-translate/core";
import { Language } from "./global";

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
  `
})
export class App implements AfterViewInit, OnInit {

  isMenuCollapsed: boolean = false;

  constructor(private _state: GlobalState,
              private _imageLoader: BaImageLoaderService,
              private _spinner: BaThemeSpinner,
              private viewContainerRef: ViewContainerRef,
              private themeConfig: BaThemeConfig,
              private lang: Language,
              private translate: TranslateService) {

    themeConfig.config();

    this._loadImages();

    this._state.subscribe("menu.isCollapsed", (isCollapsed) => {
      this.isMenuCollapsed = isCollapsed;
    });
  }

  // jwtHelper: JwtHelper = new JwtHelper();
  ngOnInit() {
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
