import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { GlobalState } from "../../../global.state";
import * as _ from "underscore";

@Component({
  selector: "ba-content-top",
  styleUrls: ["./baContentTop.scss"],
  templateUrl: "./baContentTop.html"
})
export class BaContentTop {

  activePageTitle: string = "";
  private crumbs: any[] = [];

  private isMain: boolean = false;

  private routes = {
    pos_info: "/pages/points-of-sale/pos-info"
  };

  private names = {
    pos_info: { 
      title: "POS Info",
      url: "/pages/points-of-sale"
    }
  };

  private changeLanguage() {

  }

  constructor(private _state: GlobalState, private router: Router) {
    this._state.subscribe("menu.activeLink", (activeLink) => {

      console
      if(activeLink.route.path === "main") {
        this.isMain = true;
      } else {
        this.isMain = false;
      }
      this.crumbs = [];
      console.log("activeLink bacontentTop", activeLink);
      if (activeLink.route) {
        console.log("activeLink bacontentTop", activeLink);
        const crumb = Object.assign({}, { url: "" }, activeLink);

        _.each(crumb.route.paths, (item: any, index: number) => {
          if (index > 0 && index < (crumb.route.paths.length - 1)) {
            crumb.url += `${item}/`;
          } else if (index === 0 || index === (crumb.route.paths.length - 1)) {
            crumb.url += item;
          }
        });

        this.crumbs.push(crumb);
        this.activePageTitle = activeLink.title;
      } else {
        console.log("activeLink bacontentTop", activeLink);
        _.each(this.routes, (item: string) => {
          if (this.router.url === this.routes[item]) {
            this.activePageTitle = this.names[item];
          }
          if (this.router.url.toLowerCase().indexOf("main") === -1) {
            this.crumbs.push(this.activePageTitle);
          }
          // this.crumbs.push(this.activePageTitle);
        });
      }
    });
    

  }
}
