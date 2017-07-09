import { Component, OnInit, OnDestroy, Input, OnChanges } from '@angular/core';
import { AmChartsService } from '@amcharts/amcharts3-angular';

@Component({
    selector: 'amchart',
    templateUrl: 'amcharts.component.html',
    styleUrls: ['amcharts.component.scss'],
    providers: [AmChartsService],
})
export class AmChartsComponent implements OnInit, OnDestroy, OnChanges {

    constructor(private amChartsService: AmChartsService) { 
        this.chart = this.amChartsService.makeChart('chartdiv', {
                "type": "serial",
                "theme": "light",
                "marginTop":0,
                "marginRight": 80,
                "dataProvider": [],
                "valueAxes": [{
                    "axisAlpha": 0,
                    "position": "left"
                }],
                "graphs": [{
                    "id":"g1",
                    "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
                    "bullet": "round",
                    "bulletSize": 8,
                    "lineColor": "#d1655d",
                    "lineThickness": 2,
                    "negativeLineColor": "#637bb6",
                    "type": "smoothedLine",
                    "valueField": "value"
                }],
                "chartScrollbar": {
                    "graph":"g1",
                    "gridAlpha":0,
                    "color":"#ffffff",
                    "scrollbarHeight":55,
                    "backgroundAlpha":0,
                    "selectedBackgroundAlpha":0.1,
                    "selectedBackgroundColor":"#d282f2",
                    "graphFillAlpha":0,
                    "autoGridCount":true,
                    "selectedGraphFillAlpha":0,
                    "graphLineAlpha":0.2,
                    "graphLineColor":"#c2c2c2",
                    "selectedGraphLineColor":"#ffffff",
                    "selectedGraphLineAlpha":1
                },
                "chartCursor": {
                    "categoryBalloonDateFormat": "YYYY",
                    "cursorAlpha": 0,
                    "valueLineEnabled":true,
                    "valueLineBalloonEnabled":true,
                    "valueLineAlpha":0.5,
                    "fullWidth":true
                },
                "dataDateFormat": "YYYY",
                "categoryField": "year",
                "categoryAxis": {
                    "minPeriod": "YYYY",
                    "parseDates": true,
                    "minorGridAlpha": 0.1,
                    "minorGridEnabled": true
                },
                "export": {
                    "enabled": true
                }
            });
    }

    @Input() chartData: any;
    private initFlag: boolean = false;
    private chart: any;

    ngOnInit() {
        console.log('amChart chartData', this.chartData);
        if (this.chartData !== undefined) {
            this.chart.dataProvider = this.chartData.dataProvider;
            this.initFlag = true;
        }
        
    }

    ngOnChanges() {
        if (this.chartData !== undefined) {
                this.amChartsService.updateChart( this.chart, () => {
                this.chart.dataProvider = this.chartData.dataProvider;
            });
        }
        
    }

    ngOnDestroy() { 
        this.amChartsService.destroyChart(this.chart);
    }

}
