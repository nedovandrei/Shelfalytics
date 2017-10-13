import { NgModule, ApplicationRef } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { Http, HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
// import { TranslateService } from "@ngx-translate/core";
// import { Http, HttpModule } from "@angular/http";

// translations 
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { TranslateService } from "@ngx-translate/core";

// import { AppTranslationModule } from "./app.translation.module";
/*
 * Platform and Environment providers/directives/pipes
 */
import { routing } from "./app.routing";

// App is our top level component
import { App } from "./app.component";
import { AppState, InternalStateType } from "./app.service";
import { GlobalState } from "./global.state";
import { NgaModule } from "./theme/nga.module";
import { PagesModule } from "./pages/pages.module";
import { AuthModule } from "./auth/auth.module";
import { AuthService } from "./auth/auth.service";
import { Language } from "./global";


export function createTranslateLoader(http: Http) {
    return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

const translationOptions = {
  loader: {
    provide: TranslateLoader,
    useFactory: (createTranslateLoader),
    deps: [Http]
  }
};

// Application wide providers
const APP_PROVIDERS = [
  AppState,
  GlobalState,
  AuthService,
  Language,
  TranslateService
];

export type StoreType = {
  state: InternalStateType,
  restoreInputValues: () => void,
  disposeOldHosts: () => void
};

/**
 * `AppModule` is the main entry point into Angular2's bootstraping process
 */
@NgModule({
  bootstrap: [App],
  declarations: [
    App
  ],
  imports: [ // import Angular's modules

    BrowserModule,
    HttpModule,
    RouterModule,
    FormsModule,
    // AppTranslationModule,
    TranslateModule.forRoot(translationOptions),
    ReactiveFormsModule,
    NgaModule.forRoot(),
    NgbModule.forRoot(),
    PagesModule,
    AuthModule,
    routing
  ],
  providers: [ // expose our Services and Providers into Angular's dependency injection
    APP_PROVIDERS
  ]
})

export class AppModule {

  constructor(public appState: AppState, public translate: TranslateService) {
    const lang = localStorage.getItem("language");
    translate.addLangs(["en", "ru"]);
    translate.setDefaultLang("en");
    if (lang) {
      translate.use(lang);
    } else {
      translate.use("en");
    }

    translate.onTranslationChange.subscribe((params: any) => {
      // console.log("translation changed", params);
    });
    translate.onLangChange.subscribe((params: any) => {
      // console.log(params);
      localStorage.setItem("language", params.lang);
      
    });
  }
}
