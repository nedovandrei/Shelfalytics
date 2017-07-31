import { Component, OnInit, OnDestroy, Input, OnChanges, AfterViewInit } from "@angular/core";
import { AmChartsService } from "@amcharts/amcharts3-angular";
import { AmChartConfig, IAmChartGraph } from "./amcharts.model";
import * as _ from "underscore";

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
        this.chartConfig.categoryField = this.chartData.legendField;
        this.chartConfig.graphs = _.map(this.chartData.valueFields, (item: any, index: number) => {
            const returnItem: IAmChartGraph = {
                id: `g${index}`,
                balloonText: "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
                fillAlphas: 0.9,
                lineAlpha: 0.2,
                lineColor: "#2dacd1",
                negativeLineColor: "#2dacd1",
                type: "column",
                valueField: item
            };
            return returnItem;
        });
        this.initFlag = true;

        // if (this.chartData !== undefined) {
        //     this.chartConfig["dataProvider"] = this.chartData.dataProvider;
        // } else {
        //     this.chartConfig["dataProvider"] = [];
        // }

    }

    ngOnChanges() {
        if (this.chart === undefined) {
            if (this.chartData.dataProvider === undefined || this.chartData.dataProvider.length === 0) {
                const noData: any = {};
                Object.defineProperty(noData, this.chartData.legendField, {
                    value: "No Data"
                });
                _.each(this.chartData.valueFields, (item: string) => {
                    Object.defineProperty(noData, item, {
                        value: 0
                    });
                });
                this.chartConfig["dataProvider"] = [noData];
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
