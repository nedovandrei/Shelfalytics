import { Component, OnInit, OnDestroy, Input, OnChanges, AfterViewInit } from "@angular/core";
import { AmChartsService } from "@amcharts/amcharts3-angular";





@Component({
  selector: "amchartPieCrartMain",
  template: `<div id="piechartdiv" [style.width.%]="100" [style.height.px]="400"></div>`,
  providers: [AmChartsService],
})
export class AmChartsPieChartMainComponent {
  private chart: any;

  constructor(private AmCharts: AmChartsService) {}

  ngOnInit() {
    this.chart = this.AmCharts.makeChart("piechartdiv", {
      "type": "pie",
	"balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>",
	"innerRadius": "40%",
	"titleField": "category",
	"valueField": "column-1",
	"allLabels": [],
	"balloon": {},
	"legend": {
		"enabled": true,
		"align": "center",
		"markerType": "circle"
	},
	"titles": [],
	"dataProvider": [
		{
			"category": "category 1",
			"column-1": 8
		},
		{
			"category": "category 2",
			"column-1": 6
		},
		{
			"category": "category 3",
			"column-1": 2
		}
    
	]
    });
  }

  ngOnDestroy() {
    this.AmCharts.destroyChart(this.chart);
  }
}
