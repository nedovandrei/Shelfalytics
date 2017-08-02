import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { GlobalState } from "../../../global.state";
import * as _ from "underscore";

@Component({
  selector: "ba-content-top",
  styleUrls: ["./baContentTop.scss"],
  templateUrl: "./baContentTop.html",
})
export class BaContentTop {

  public activePageTitle: string = "";
  private crumbs: any[] = [];

  private routes = {
    pos_info: "/pages/points-of-sale/pos-info"
  };

  private names = {
    pos_info: { 
      title: "POS Info",
      url: "/pages/points-of-sale"
    }
  };

  constructor(private _state: GlobalState, private router: Router) {
    this._state.subscribe("menu.activeLink", (activeLink) => {
      this.crumbs = [];
      console.log("activeLink bacontentTop", activeLink);
      if (activeLink.route) {

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
          this.crumbs.push(this.activePageTitle);
        });
      }
    });
    
  }
}
