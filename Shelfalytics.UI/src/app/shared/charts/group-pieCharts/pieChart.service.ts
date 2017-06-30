import {Injectable} from '@angular/core';
import {BaThemeConfigProvider, colorHelper} from '../../../theme';

@Injectable()
export class PieChartService {

  constructor(private _baConfig:BaThemeConfigProvider) {
  }

  getData(data: any, rowCount: number) {
    console.log("get data - data", data);
    let pieColor = this._baConfig.get().colors.custom.dashboardPieChart;

    let outputData: any[] = [];
    for (var i = 0; i < rowCount; i++){
      let arrayData: any;
      if(data[i] !== undefined){
        arrayData = {
          isEmpty: false,
          color: pieColor,
          description: data[i].ProductName,
          name: data[i].SKUName,
          stats: data[i].Percentage,
          icon: "beer"
        }
      } else {
        arrayData = {
          isEmpty: true
        }
      }

      outputData.push(arrayData);
    }
    console.log("outputData", outputData);
    return outputData;
    // return [
    //   {
    //     color: pieColor,
    //     description: 'dashboard.new_visits',
    //     stats: '57,820',
    //     icon: 'person',
    //   }, {
    //     color: pieColor,
    //     description: 'dashboard.purchases',
    //     stats: '$ 89,745',
    //     icon: 'money',
    //   }, {
    //     color: pieColor,
    //     description: 'dashboard.active_users',
    //     stats: '178,391',
    //     icon: 'face',
    //   }, {
    //     color: pieColor,
    //     description: 'dashboard.returned',
    //     stats: '32,592',
    //     icon: 'refresh',
    //   }
    // ];
  }
}
