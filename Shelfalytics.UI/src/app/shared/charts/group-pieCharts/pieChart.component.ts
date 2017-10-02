import { Component, Input, OnInit, AfterViewInit, OnChanges } from "@angular/core";

import { PieChartService } from "./pieChart.service";
import { global } from "../../../global";
import * as _ from "underscore";

import "easy-pie-chart/dist/jquery.easypiechart.js";

@Component({
  selector: "pie-chart",
  templateUrl: "./group-pieChart.html",
  styleUrls: ["./pieChart.scss"],
  providers: [PieChartService]
})
// TODO: move easypiechart to component
export class GroupPieChart implements OnInit, AfterViewInit, OnChanges {

  @Input() chartData: any;
  @Input() rowCount: number;

  charts: Object[];
  private _init = false;
  private firstLoad = true;

  private chartArrays: any[];
  private imagePath: string = global.imagePath;

  constructor(private _pieChartService: PieChartService) { }
  ngOnInit() {


    console.log("Group pie charts chart data", this.chartData);

    let chartsArray = [];
    let pusherIndex = 0;
    for (let i = 0; i < this.chartData.YCount; i++) {
      let oneRowArray = [];

      let equipmentRemainigWidth = this.chartData.Width - 3;
      for (let j = pusherIndex; j < this.chartData.RowInfo.length; j++, pusherIndex++) {
        if (this.chartData.RowInfo[j].BottleDiameter < equipmentRemainigWidth) {
          const pusher = {
            BottleDiameter: this.chartData.RowInfo[j].BottleDiameter,
            Percentage: this.chartData.RowInfo[j].Percentage,
            ProductName: this.chartData.RowInfo[j].ProductName,
            Row: this.chartData.RowInfo[j].Row,
            SKUName: this.chartData.RowInfo[j].SKUName,
            WidthPercentage: this.chartData.RowInfo[j].BottleDiameter / this.chartData.Width * 100,
            PhotoPath: this.chartData.RowInfo[j].PhotoPath
          };
          oneRowArray.push(pusher);
          if (j !== this.chartData.RowInfo.length - 2) {
            equipmentRemainigWidth -= pusher.BottleDiameter;
          }

        } else {
          break;
        }
      }
      chartsArray.push(oneRowArray);
    }

    const targetArray = [];
    for (let i = 0; i < chartsArray.length; i++) {
      let arrayWithMargins = {
        width: 0,
        data: chartsArray[i]
      };
      let percentageSumm = 0;
      for (let j = 0; j < arrayWithMargins.data.length; j++) {
        percentageSumm += arrayWithMargins.data[j].WidthPercentage;
      }

      arrayWithMargins.width = (100 - percentageSumm) / (arrayWithMargins.data.length - 1);
      targetArray.push(arrayWithMargins);
    }
    console.log("equipmentInfo [][] array ", chartsArray);
    this.chartArrays = targetArray;

    this.charts = this._pieChartService.getData(this.chartData.RowInfo, this.rowCount);
    this._init = true;
    this.firstLoad = false;
  }

  ngAfterViewInit() {
    this._loadPieCharts();
    // this._updatePieCharts();
  }

  private _loadPieCharts() {

    jQuery(".group-chart").each(function () {
      const chart = jQuery(this);
      chart.easyPieChart({
        easing: "easeOutBounce",
        onStep: function (from, to, percent) {
          jQuery(this.el).find(".percent").text(Math.round(percent));
        },
        barColor: jQuery(this).attr("data-rel"),
        trackColor: "rgba(0,0,0,0)",
        size: 84,
        scaleLength: 0,
        animation: 2000,
        lineWidth: 9,
        lineCap: "round",
      });
    });
  }

  ngOnChanges() {
    if (!this.firstLoad){

      this._updatePieCharts();
      let chartsArray = [];
      let pusherIndex = 0;
      for (let i = 0; i < this.chartData.YCount; i++) {
        let oneRowArray = [];
  
        let equipmentRemainigWidth = this.chartData.Width - 3;
        for (let j = pusherIndex; j < this.chartData.RowInfo.length; j++, pusherIndex++) {
          if (this.chartData.RowInfo[j].BottleDiameter < equipmentRemainigWidth) {
            const pusher = {
              BottleDiameter: this.chartData.RowInfo[j].BottleDiameter,
              Percentage: this.chartData.RowInfo[j].Percentage,
              ProductName: this.chartData.RowInfo[j].ProductName,
              Row: this.chartData.RowInfo[j].Row,
              SKUName: this.chartData.RowInfo[j].SKUName,
              WidthPercentage: this.chartData.RowInfo[j].BottleDiameter / this.chartData.Width * 100,
              PhotoPath: this.chartData.RowInfo[j].PhotoPath
            };
            oneRowArray.push(pusher);
            if (j !== this.chartData.RowInfo.length - 2) {
              equipmentRemainigWidth -= pusher.BottleDiameter;
            }
  
          } else {
            break;
          }
        }
        chartsArray.push(oneRowArray);
      }
  
      const targetArray = [];
      for (let i = 0; i < chartsArray.length; i++) {
        let arrayWithMargins = {
          width: 0,
          data: chartsArray[i]
        };
        let percentageSumm = 0;
        for (let j = 0; j < arrayWithMargins.data.length; j++) {
          percentageSumm += arrayWithMargins.data[j].WidthPercentage;
        }
  
        arrayWithMargins.width = (100 - percentageSumm) / (arrayWithMargins.data.length - 1);
        targetArray.push(arrayWithMargins);
      }
      console.log("equipmentInfo [][] array ", chartsArray);
      this.chartArrays = targetArray;
  
      this.charts = this._pieChartService.getData(this.chartData.RowInfo, this.rowCount);
    }
  }

  private _updatePieCharts() {
    jQuery(".pie-charts .group-chart").each(function(index, chart) {
      jQuery(chart).data("easyPieChart").update(parseInt(jQuery(chart).attr("data-percent")));
    });
  }
}
