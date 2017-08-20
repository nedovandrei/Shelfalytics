// export enum IAmChartType {
//     serial = "serial",
//     pie = "pie"
// }

export interface IAmChartConfig {
    type: string;
    pathToImages: string;
    theme?: string;
    marginTop?: number;
    marginRight?: number;
    valueAxes: IAmChartValueAxis[];
    graphs?: IAmChartGraph[];
    chartScrollbar: IAmChartScrollBar;
    chartCursor: IAmChartCursor;
    chartLegend?: IAmChartLegend;
    dataDateFormat?: string;
    categoryField?: string;
    valueField?: string;
    titleField?: string;
    categoryAxis?: IAmChartCategoryAxis;
    export?: IAmChartExportParam;
    dataProvider: any[];
    color?: string;
    labelsEnabled?: boolean;


    innerRadius?: number;
    hoverAlpha?: number;
    outlineThickness?: number;
    depth3D?: number;
    angle?: number;
    labelRadius?: number;
    balloonText?: string;
    fontSize?: number;
    fontFamily?: string;
    colors?: any[];
    position? : string;
    valueText?: string;
    valueAlign? : string;
    valueWidth? : number;
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
    autoColor?: boolean;
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

export interface IAmChartLegend {
  position? : string;
  valueText? : string;
  valueAlign? : string;
  valueWidth? : number;
}

export class AmChartConfig implements IAmChartConfig {
    type: string;
    pathToImages: string;
    theme?: string;
    color?: string;
    marginTop?: number;
    marginRight?: number;
    valueAxes: IAmChartValueAxis[];
    graphs?: IAmChartGraph[];
    chartScrollbar: IAmChartScrollBar;
    chartCursor: IAmChartCursor;
    chartLegend?: IAmChartLegend;
    dataDateFormat?: string;
    rotate?: boolean;
    categoryField?: string;
    valueField?: string;
    titleField?: string;
    categoryAxis?: IAmChartCategoryAxis;
    export?: IAmChartExportParam;
    dataProvider: any[];
    labelsEnabled?: boolean;

    innerRadius?: number;
    hoverAlpha?: number;
    outlineThickness?: number;
    depth3D?: number;
    angle?: number;
    labelRadius?: number;
    balloonText?: string;
    fontSize?: number;
    fontFamily?: string;
    colors?: any[];
    position? : string;
    valueText?: string;
    valueAlign? : string;
    valueWidth? : number;

    constructor(chartType: string) {
        this.type = chartType;
        this.pathToImages = "assets/images/";
        this.theme = "dark";
        this.color = "#000000";
        this.marginTop = 0;
        this.marginRight = 0;
        this.fontSize = 15;
        this.fontFamily = "Roboto";
        this.colors = ["#67b7dc", "#fdd400", "#84b761", "#cc4748", "#cd82ad", "#2f4074", "#448e4d", "#b7b83f", "#b9783f", "#b93e3d", "#913167","#666","#777"];
        if (this.type === "serial") {
            this.valueAxes = [{
                axisAlpha: 0,
                position: "top"
            }];
            this.rotate = true;
            this.graphs = [];
            this.chartScrollbar = {
                "graph": "g1",
                "gridAlpha": 0,
                "color": "transparent",
                "scrollbarHeight": 55,
                "backgroundAlpha": 0,
                "selectedBackgroundAlpha": 1,
                "selectedBackgroundColor": "#2dacd1",
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
            this.categoryAxis = {
                // "minPeriod": "YYYY",
                // "parseDates": true,
                "position": "left",
                "minorGridAlpha": 0.9,
                "minorGridEnabled": true,
                "axisColor": "#FFFFFF"
            };
        } else if (this.type === "pie") {
          this.chartLegend = {
            "position": 'right',

            "valueText" : '[value]',
            "valueAlign" : 'right',
            "valueWidth" : 120
          }
            // this.innerRadius = 50;
            this.hoverAlpha = 0.85;
            this.outlineThickness = 1;
            this.labelRadius = -50;
            this.balloonText =
                "[[title]]<br><b>[[value]]%</b><br><span style='font-size:9px'>[[percents]]% of all OOS</span>";
            // this.depth3D = 12;
            // this.angle = 39.6;
            // this.labelsEnabled = false;


        }

        this.export = {
            enabled: true
        };
    }
}
