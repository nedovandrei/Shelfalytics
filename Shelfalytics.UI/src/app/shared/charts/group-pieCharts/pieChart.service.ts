import { Injectable } from "@angular/core";
import { BaThemeConfigProvider, colorHelper } from "../../../theme";

@Injectable()
export class PieChartService {

  constructor(private _baConfig: BaThemeConfigProvider) {
  }

  getData(data: any, rowCount: number) {
    // console.log("get data - data", data);
    const pieColor = this._baConfig.get().colors.custom.dashboardPieChart;

    const outputData: any[] = [];
    for (let i = 0; i < rowCount; i++) {
      let arrayData: any;
      if (data[i] !== undefined) {
        arrayData = {
          isEmpty: false,
          color: pieColor,
          description: data[i].ProductName,
          name: data[i].SKUName,
          stats: data[i].Percentage,
          row: i + 1,
          icon: "beer"
        };
      } else {
        arrayData = {
          isEmpty: true,
          row: i + 1
        };
      }

      outputData.push(arrayData);
    }
    // console.log("outputData", outputData);
    return outputData;
  }
}
