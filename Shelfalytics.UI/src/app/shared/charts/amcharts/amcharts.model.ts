export interface IAmChartConfig {
    type: string;
    theme?: string;
    marginTop?: number;
    marginRight?: number;
    valueAxes: IAmChartValueAxis[];
    graphs: IAmChartGraph[];
    chartScrollbar: IAmChartScrollBar;
    chartCursor: IAmChartCursor;
    dataDateFormat?: string;
    categoryField: string;
    categoryAxis?: IAmChartCategoryAxis;
    export?: IAmChartExportParam;
    dataProvider: any[];
    color?: string;
}

export interface IAmChartValueAxis {
    axisAlpha: number;
    position: string;
}

export interface IAmChartGraph {
    id: string;
    balloonText?: string;
    bullet?: string;
    bulletSize?: number;
    lineColor?: string;
    lineAlpha?: number;
    fillAlphas?: number;
    lineThickness?: number;
    negativeLineColor?: string;
    type?: string;
    valueField: string;
}

export interface IAmChartScrollBar {
    dragIcon?: string;
    graph: string;
    gridAlpha: number;
    color: string;
    scrollbarHeight?: number;
    backgroundAlpha?: number;
    selectedBackgroundAlpha?: number;
    selectedBackgroundColor?: string;
    graphFillAlpha?: number;
    autoGridCount?: boolean;
    selectedGraphFillAlpha?: number;
    graphLineAlpha?: number;
    graphLineColor?: string;
    selectedGraphLineColor?: string;
    selectedGraphLineAlpha?: number;
}

export interface IAmChartCursor {
    categoryBalloonDateFormat?: string;
    cursorAlpha?: number;
    valueLineEnabled?: boolean;
    valueLineBalloonEnabled?: boolean;
    valueLineAlpha?: number;
    fullWidth?: boolean;
    categoryBalloonColor?: string;
    cursorColor?: string;
}

export interface IAmChartCategoryAxis {
    position?: string;
    minPeriod?: string;
    parseDates?: boolean;
    minorGridAlpha?: number;
    minorGridEnabled?: boolean;
    axisColor?: string;
}

export interface IAmChartExportParam {
    enabled: boolean;
}

export class AmChartConfig implements IAmChartConfig {
    type: string;
    theme?: string;
    color?: string;
    marginTop?: number;
    marginRight?: number;
    valueAxes: IAmChartValueAxis[];
    graphs: IAmChartGraph[];
    chartScrollbar: IAmChartScrollBar;
    chartCursor: IAmChartCursor;
    dataDateFormat?: string;
    rotate?: boolean;
    categoryField: string;
    categoryAxis?: IAmChartCategoryAxis;
    export?: IAmChartExportParam;
    dataProvider: any[];

    constructor(chartType: string) {
        this.type = chartType;
        this.theme = "dark";
        this.color = "#ffffff";
        this.marginTop = 0;
        this.marginRight = 80;
        this.valueAxes = [{
            axisAlpha: 0,
            position: "top"
        }];
        this.rotate = true;
        this.graphs = [
            // {
            //     "id": "g1",
            //     "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
            //     // "bullet": "round",
            //     // "bulletSize": 8,
            //     "fillAlphas": 0.9,
            //     "lineAlpha": 0.2,
            //     "lineColor": "#2dacd1",
            //     // "lineThickness": 2,
            //     "negativeLineColor": "#2dacd1",
            //     "type": "column",
            //     "valueField": "valueLol"
            // },
            // {
            //     "id": "g2",
            //     "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
            //     "fillAlphas": 0.9,
            //     "lineAlpha": 0.2,
            //     "lineColor": "#90b900",
            //     "lineThickness": 2,
            //     "negativeLineColor": "#90b900",
            //     "type": "column",
            //     "valueField": "valueKek"
            // }
        ];
        this.chartScrollbar = {
            "dragIcon": "../../assets/icon/dragIconRoundBig",
            "graph": "g1",
            "gridAlpha": 0,
            "color": "#ffffff",
            "scrollbarHeight": 55,
            "backgroundAlpha": 0,
            "selectedBackgroundAlpha": 0.1,
            "selectedBackgroundColor": "#d282f2",
            "graphFillAlpha": 0,
            "autoGridCount": true,
            "selectedGraphFillAlpha": 0,
            "graphLineAlpha": 0.2,
            "graphLineColor": "#c2c2c2",
            "selectedGraphLineColor": "#ffffff",
            "selectedGraphLineAlpha": 1,
        };
        this.chartCursor = {
            "categoryBalloonDateFormat": "YYYY",
            "cursorAlpha": 0,
            "valueLineEnabled": true,
            "valueLineBalloonEnabled": true,
            "valueLineAlpha": 0.5,
            "fullWidth": true,
            "categoryBalloonColor": "#2dacd1", // цвет заливки указателей
            "cursorColor": "#2dacd1" // цвет линии, тоже желательно менять под стать верхнему
        };
        this.dataDateFormat = "YYYY";
        // this.categoryField = "year";
        this.categoryAxis = {
            // "minPeriod": "YYYY",
            // "parseDates": true,
            "position": "left",
            "minorGridAlpha": 0.9,
            "minorGridEnabled": true,
            "axisColor": "#FFFFFF"
        };
        this.export = {
            enabled: true
        };
    }
}
