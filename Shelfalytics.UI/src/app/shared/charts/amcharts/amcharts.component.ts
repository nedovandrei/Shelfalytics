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

    @Input() chartType: string;
    @Input() chartData: any;
    @Input() chartName: string;
    private initFlag: boolean = false;
    private chart: any;
    private sortDirection: "asc" | "desc" = "desc";

    private chartConfig: any;

    ngAfterViewInit() {
        this.initChart();
        this.sortData();
    }

    ngOnInit() {
        this.chartConfig = new AmChartConfig(this.chartType ? this.chartType : "serial");
        this.chartConfig.categoryField = this.chartData.legendField;
        if (this.chartType === "pie") {
            this.chartConfig.titleField = this.chartData.legendField;
            this.chartConfig.valueField = this.chartData.valueFields[0];
        } else {
            this.chartConfig.graphs = _.map(this.chartData.valueFields, (item: any, index: number) => {
                const returnItem: IAmChartGraph = {
                    id: `g${index}`,
                    balloonText: "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
                    fillAlphas: 0.9,
                    lineAlpha: 0.2,
                    // lineColor: "#2dacd1",
                    // negativeLineColor: "#2dacd1",
                    type: "column",
                    valueField: item


                };
                return returnItem;
            });
        }

        this.initFlag = true;

        // if (this.chartData !== undefined) {
        //     this.chartConfig["dataProvider"] = this.chartData.dataProvider;
        // } else {
        //     this.chartConfig["dataProvider"] = [];
        // }

    }

    ngOnChanges() {
        this.initializeDataProvider();
    }

    private initializeDataProvider() {
        if (this.chartConfig !== undefined) {
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

    }

    private initChart() {
        this.chart =
            this.amChartsService.makeChart(this.chartName ? this.chartName : "chartdiv", this.chartConfig );
    }

    private sortData() {
        if (this.chartData.dataProvider.length !== 0) {
            const sortedArr = _.sortBy(this.chartData.dataProvider, (item: any) => {
                return item[this.chartData.valueFields[0]];
            });
            if (this.sortDirection === "asc") {
                this.chartData.dataProvider = sortedArr;
                this.initializeDataProvider();
            } else {
                this.chartData.dataProvider = sortedArr.reverse();
                this.initializeDataProvider();
            }
        }

    }

    ngOnDestroy() {
        this.amChartsService.destroyChart(this.chart);
    }

}
