import { Component, OnInit, OnDestroy, Input, OnChanges, AfterViewInit } from "@angular/core";
import { AmChartsService } from "@amcharts/amcharts3-angular";
import { AmChartConfig } from "./amcharts.model";

@Component({
    selector: "amchart",
    templateUrl: "amcharts.component.html",
    styleUrls: ["amcharts.component.scss"],
    providers: [AmChartsService],
})
export class AmChartsComponent implements OnInit, OnDestroy, OnChanges, AfterViewInit {

    constructor(private amChartsService: AmChartsService) { }

    @Input() chartData: any;
    private initFlag: boolean = false;
    private chart: any;

    private chartConfig = new AmChartConfig("serial");
    // private chartConfig = {
    //     "type": "serial",
    //     "theme": "light",
    //     "marginTop": 0,
    //     "marginRight": 80,
    //     "valueAxes": [{
    //         "axisAlpha": 0,
    //         "position": "left"
    //     }],
    //     "graphs": [
    //         {
    //             "id": "g1",
    //             "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
    //             "bullet": "round",
    //             "bulletSize": 8,
    //             "lineColor": "#d1655d",
    //             "lineThickness": 2,
    //             "negativeLineColor": "#637bb6",
    //             "type": "smoothedLine",
    //             "valueField": "valueLol"
    //         },
    //         {
    //             "id": "g2",
    //             "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
    //             "bullet": "round",
    //             "bulletSize": 8,
    //             "lineColor": "yellow",
    //             "lineThickness": 2,
    //             "negativeLineColor": "blue",
    //             "type": "smoothedLine",
    //             "valueField": "valueKek"
    //         },
    //     ],
    //     "chartScrollbar": {
    //         "dragIcon": "../../assets/icon/dragIconRoundBig",
    //         "graph": "g1",
    //         "gridAlpha": 0,
    //         "color": "#ffffff",
    //         "scrollbarHeight": 55,
    //         "backgroundAlpha": 0,
    //         "selectedBackgroundAlpha": 0.1,
    //         "selectedBackgroundColor": "#d282f2",
    //         "graphFillAlpha": 0,
    //         "autoGridCount": true,
    //         "selectedGraphFillAlpha": 0,
    //         "graphLineAlpha": 0.2,
    //         "graphLineColor": "#c2c2c2",
    //         "selectedGraphLineColor": "#ffffff",
    //         "selectedGraphLineAlpha": 1,
    //     },
    //     "chartCursor": {
    //         "categoryBalloonDateFormat": "YYYY",
    //         "cursorAlpha": 0,
    //         "valueLineEnabled": true,
    //         "valueLineBalloonEnabled": true,
    //         "valueLineAlpha": 0.5,
    //         "fullWidth": true
    //     },
    //     "dataDateFormat": "YYYY",
    //     "categoryField": "year",
    //     "categoryAxis": {
    //         "minPeriod": "YYYY",
    //         "parseDates": true,
    //         "minorGridAlpha": 0.1,
    //         "minorGridEnabled": true
    //     },
    //     "export": {
    //         "enabled": true
    //     }
    // };

    ngAfterViewInit() {
        this.initChart();
    }

    ngOnInit() {
        // if (this.chartData !== undefined) {
        //     this.chartConfig["dataProvider"] = this.chartData.dataProvider;            
        // } else {
        //     this.chartConfig["dataProvider"] = [];
        // }

    }

    ngOnChanges() {
        if (this.chart === undefined) {
            if (this.chartData === undefined) {
                this.chartConfig["dataProvider"] = [];
            } else {
                this.chartConfig["dataProvider"] = this.chartData.dataProvider;
            }
        } else {
            this.amChartsService.updateChart( this.chart, () => {
                this.chart.dataProvider = this.chartData.dataProvider;
            });
        }
    }

    private initChart() {
        this.chart = this.amChartsService.makeChart("chartdiv", this.chartConfig );
    }

    ngOnDestroy() {
        this.amChartsService.destroyChart(this.chart);
    }

}