export interface IAmChartConfig {
    type: string;
    theme?: string;
    marginTop?: number;
    marginRight?: number;
    valueAxes: IAmChartValueAxis[];
    graphs: IAmChartGraph[];
    chartScrollBar: IAmChartScrollBar;
    chartCursor: IAmChartCursor;
    dataDateFormat?: string;
    categoryField: string;
    categoryAxis?: IAmChartCategoryAxis;
    export?: IAmChartExportParam;
    dataProvider: any[];
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
}

export interface IAmChartCategoryAxis {
    minPeriod?: string;
    parseDates?: boolean;
    minorGridAlpha?: number;
    minorGridEnabled?: boolean;
}

export interface IAmChartExportParam {
    enabled: boolean;
}

export class AmChartConfig implements IAmChartConfig {
    type: string;
    theme?: string;
    marginTop?: number;
    marginRight?: number;
    valueAxes: IAmChartValueAxis[];
    graphs: IAmChartGraph[];
    chartScrollBar: IAmChartScrollBar;
    chartCursor: IAmChartCursor;
    dataDateFormat?: string;
    categoryField: string;
    categoryAxis?: IAmChartCategoryAxis;
    export?: IAmChartExportParam;
    dataProvider: any[];

    constructor(chartType: string) {
        this.type = chartType;
        this.theme = "light";
        this.marginTop = 0;
        this.marginRight = 80;
        this.valueAxes = [{
            axisAlpha: 0,
            position: "left"
        }];
        this.graphs = [
            {
                "id": "g1",
                "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
                "bullet": "round",
                "bulletSize": 8,
                "lineColor": "#d1655d",
                "lineThickness": 2,
                "negativeLineColor": "#637bb6",
                "type": "smoothedLine",
                "valueField": "valueLol"
            },
            {
                "id": "g2",
                "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
                "bullet": "round",
                "bulletSize": 8,
                "lineColor": "yellow",
                "lineThickness": 2,
                "negativeLineColor": "blue",
                "type": "smoothedLine",
                "valueField": "valueKek"
            }
        ];
        this.chartScrollBar = {
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
            "fullWidth": true
        };
        this.dataDateFormat = "YYYY";
        this.categoryField = "year";
        this.categoryAxis = {
            "minPeriod": "YYYY",
            "parseDates": true,
            "minorGridAlpha": 0.1,
            "minorGridEnabled": true
        };
        this.export = {
            enabled: true
        };
    }
}
