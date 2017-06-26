import { Component, OnInit } from '@angular/core';
import { GridstackOptions } from "./gridstack.model";
import "../../../../../node_modules/gridstack/dist/gridstack.min.js";
import * as $ from "jquery";
import * as _ from "underscore";

@Component({
  selector: 'gridstack',
  templateUrl: './gridstack.component.html',
  styleUrls: [
    './gridstack.component.scss',
    "../../../../../node_modules/gridstack/dist/gridstack.min.css"
  ]
})
export class GridstackComponent implements OnInit {

  constructor() { }

  private gridstackOptions: GridstackOptions = {
    cellHeight: 80,
    animate: true,
    alwaysShowResizeHandle: true
  };

  ngOnInit() {
    $(".grid-stack").gridstack(this.gridstackOptions);
  }

  // private save(event: any){
  //     console.log("save event", event);
  // }
  //
  // private deletePanel(){
  //   console.log("deletePanel event");
  // }


}
