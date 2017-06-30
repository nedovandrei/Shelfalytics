import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { TestService } from "../test.service";
import * as moment from "moment";

@Component({
  selector: 'equipment-info',
  templateUrl: './equipment-info.component.html',
  styleUrls: ['./equipment-info.component.scss'],
  providers: [TestService]
})
export class EquipmentInfoComponent implements OnInit {

  constructor(private testService: TestService) { }

  @Input() equipmentId: number;

  private initFlag: boolean = false;
  private equipmentData: any;
  private timeStamp: string;

  ngOnInit() {

  }

  ngOnChanges(){
    this.initFlag = false;
    this.init();
  }

  private init(){
    this.testService.getShelfData(this.equipmentId).subscribe((data: any) => {
      this.equipmentData = data[0];
      
      this.timeStamp = moment(data[0].TimeStamp).format("hh:mm:ss A, dddd, Do MMMM YYYY");
      this.initFlag = true;
    });
  }

}
