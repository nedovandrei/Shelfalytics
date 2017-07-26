import { Component, OnInit, OnDestroy } from '@angular/core';
import { PosInfoService } from './pos-info.service';
import { ActivatedRoute } from "@angular/router";
import * as moment from 'moment';

@Component({
  selector: 'pos-info',
  templateUrl: './pos-info.component.html',
  styleUrls: ['./pos-info.component.scss'],
  providers: [PosInfoService]
})
export class PosInfoComponent implements OnInit, OnDestroy {

  constructor(private posInfoService: PosInfoService, private route: ActivatedRoute) { }

  private initFlag: boolean = false;
  private posData: any;
  private equipmentInFocus: number = 0;
  private openHours: string;
  private closeHours: string;
  private paramSubscription: any;
  private id: number;

  // private OOSChartOptions = {
  //   // labels: [ "Out Of Stock" ],
  //   series: []
  // }

  private OOSChartData = {};

  private sampleChartData = {
    dataProvider: [
      {
        year: '2005',
        valueLol: 35,
        valueKek: 12
      },
      {
        year: '2006',
        valueLol: 43,
        valueKek: 9,
      },
      {
        year: '2007',
        valueLol: 24,
        valueKek: 89,
      },
      {
        year: "2008",
        valueLol: 77,
        valueKek: 28,
      },
    ],
  };

  ngOnInit() {
    this.paramSubscription = this.route.params.subscribe(params => {
       this.id = parseInt(params['id']); // (+) converts string 'id' to a number

       this.posInfoService.getPointOfSaleData(this.id).subscribe((data: any) => {
        this.posData = data[0];
        this.equipmentInFocus = this.posData.EquipmentIds[0];

        this.openHours = moment(this.posData.OpeningHours).format('hh:mm A');
        this.closeHours = moment(this.posData.ClosingHours).format('hh:mm A');

        if (this.equipmentInFocus) {
          this.posInfoService.getOOSPercentage(this.equipmentInFocus).subscribe((percentage: number) => {
            // this.OOSChartOptions.series.push(percentage);
            // this.OOSChartOptions.series.push(100);

            this.OOSChartData = {
              data: percentage,
              color: percentage < 20 ? 'rgba(255, 255, 255, 1)' :
                percentage < 50 ? 'rgba(244,198,61, 1)' : 'rgba(215,2,6, 1)'
            };

            this.initFlag = true;
          });
        } else {
          this.initFlag = true;
        }

      });
    });





    // this._testService.getShelfData().subscribe((data: any) => {
    //   this.equipmentData = data[0];
    //   this.initFlag = true;
    // });
  }

  private changeEquipmentInFocus(equipmentId: number) {
    this.equipmentInFocus = equipmentId;
  }

  ngOnDestroy() {
    this.paramSubscription.unsubscribe();
  }
  
}
