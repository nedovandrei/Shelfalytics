import { Component, OnInit, OnDestroy, Input, OnChanges, AfterViewInit } from "@angular/core";
import { AmChartsService } from "@amcharts/amcharts3-angular";





@Component({
  selector: "amchartMain",
  template: `<div id="chartdiv" [style.width.%]="100" [style.height.px]="400"></div>`,
  providers: [AmChartsService],
})
export class AmChartsMainComponent {
  private chart: any;

  constructor(private AmCharts: AmChartsService) {}

  ngOnInit() {
    this.chart = this.AmCharts.makeChart("chartdiv", {
      "type": "serial",
	"categoryField": "category",
  "color": "#FFF",
	"rotate": true,
	"startDuration": 1,
	"categoryAxis": {
		"gridPosition": "start"
	},
	"chartCursor": {
		"enabled": true,
    "categoryBalloonColor": "#2dacd1", // цвет заливки указателей
    "cursorColor": "#2dacd1" // цвет линии, тоже желательно менять под стать верхнему
	},
	"chartScrollbar": {
		"enabled": true
	},
	"trendLines": [

  ],
	"graphs": [
		{
			"fillAlphas": 1,
			"id": "AmGraph-1",
			"title": "graph 1",
			"type": "column",
			"valueField": "column-1",
      "lineColor": "#2dacd1",
      "negativeLineColor": "#2dacd1",
		}
	],
	"guides": [],
	"valueAxes": [
		{
			"id": "ValueAxis-1",
			"title": ""
		}
	],
	"allLabels": [],
	"balloon": {},
	"dataProvider": [
		{
			"category": "category 1",
			"column-1": 8
		},
		{
			"category": "category 2",
			"column-1": 16
		},
		{
			"category": "category 3",
			"column-1": 2
		},
		{
			"category": "category 4",
			"column-1": 7
		},
		{
			"category": "category 5",
			"column-1": 5
		},
		{
			"category": "category 6",
			"column-1": 9
		},
		{
			"category": "category 7",
			"column-1": 4
		},
		{
			"category": "category 8",
			"column-1": 15
		},
		{
			"category": "category 9",
			"column-1": 12
		},
		{
			"category": "category 10",
			"column-1": 17
		},
		{
			"category": "category 11",
			"column-1": 18
		},
		{
			"category": "category 12",
			"column-1": 21
		},
		{
			"category": "category 13",
			"column-1": 24
		},
		{
			"category": "category 14",
			"column-1": 23
		},
		{
			"category": "category 15",
			"column-1": 24
		}
	]
    });
  }

  ngOnDestroy() {
    this.AmCharts.destroyChart(this.chart);
  }
}
