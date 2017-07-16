import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { PosInfoService } from '../pos-info.service';
import * as moment from 'moment';

@Component({
  selector: 'equipment-info',
  templateUrl: './equipment-info.component.html',
  styleUrls: ['./equipment-info.component.scss'],
  providers: [PosInfoService],
})
export class EquipmentInfoComponent implements OnInit, OnChanges {

  constructor(private posInfoService: PosInfoService) { }

  @Input() equipmentId: number;

  private initFlag: boolean = false;
  private equipmentData: any;
  private timeStamp: string;

  ngOnInit() {

  }

  ngOnChanges() {
    this.initFlag = false;
    this.init();
  }

  private init() {
    this.posInfoService.getShelfData(this.equipmentId).subscribe((data: any) => {
      this.equipmentData = data[0];

      this.timeStamp = moment(data[0].TimeStamp).format('hh:mm:ss A, dddd, Do MMMM YYYY');
      this.initFlag = true;
    });
  }

}
