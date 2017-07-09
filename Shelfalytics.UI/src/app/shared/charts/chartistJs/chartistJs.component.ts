import {Component, Input} from '@angular/core';

import {ChartistJsService} from './chartistJs.service';

@Component({
  selector: 'charts',
  templateUrl: './chartistJs.html',
  styleUrls: ['./chartistJs.scss'],
  providers: [ChartistJsService]
})

export class ChartistJs {

  private initFlag: boolean = false;
  @Input() chartType: string;
  @Input() chartData: any;
  @Input() chartOptions: any;

  data:any;

  constructor(private _chartistJsService:ChartistJsService) {
  }

  ngOnInit() {
    console.log("chartType ", this.chartType);
    console.log("chartData ", this.chartData);
    
    // this.data = this._chartistJsService.getAll();
    this.chartOptions = {
      fullWidth: true,
      // height: '300px',
      // weight: '300px',
      donutWidth: 40,
      donut: true
      // labelInterpolationFnc: function (value) {
      //   return value + '%';
      // }
    }

    this.initFlag = true;
  }

  getResponsive(padding, offset) {
    return this._chartistJsService.getResponsive(padding, offset);
  }
}
