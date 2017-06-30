import { Component, OnInit } from '@angular/core';
import { TestService } from "./test.service";
import * as moment from "moment";

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss'],
  providers: [TestService]
})
export class TestComponent implements OnInit {

  constructor(private _testService: TestService) { }

  private initFlag: boolean = false;
  private posData: any;
  private equipmentInFocus: number = 0;
  private openHours: string;
  private closeHours: string;

  ngOnInit() {
    this._testService.getPointOfSaleData().subscribe((data: any) =>{
      this.posData = data[0];
      this.equipmentInFocus = this.posData.EquipmentIds[0];

      this.openHours = moment(this.posData.OpeningHours).format("hh:mm A");
      this.closeHours = moment(this.posData.ClosingHours).format("hh:mm A");

      this.initFlag = true;
    });

    // this._testService.getShelfData().subscribe((data: any) => {
    //   this.equipmentData = data[0];
    //   this.initFlag = true;
    // });
  }

  private changeEquipmentInFocus(equipmentId: number){
    this.equipmentInFocus = equipmentId;
  }

}
