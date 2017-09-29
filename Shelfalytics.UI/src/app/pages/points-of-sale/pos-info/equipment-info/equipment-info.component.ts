import { Component, OnInit, Input, OnChanges } from "@angular/core";
import { PosInfoService } from "../pos-info.service";
import * as moment from "moment";

@Component({
  selector: "equipment-info",
  templateUrl: "./equipment-info.component.html",
  styleUrls: ["./equipment-info.component.scss"],
  providers: [PosInfoService],
})
export class EquipmentInfoComponent implements OnInit, OnChanges {

  constructor(private posInfoService: PosInfoService) { }

  @Input() equipmentId: number;
  @Input() oosChartData: any;
  @Input() fillChartData: any;

  private initFlag: boolean = false;
  private equipmentData: any;
  private timeStamp: string;
  private firstLoad: boolean = true;

  ngOnInit() {

  }

  ngOnChanges() {
    if(this.firstLoad){
      this.init();
    } else {
      this.reload();
    }
    
  }

  private init() {
    this.initFlag = false;
    this.posInfoService.getShelfData(this.equipmentId).subscribe((data: any) => {
      this.equipmentData = data[0];
      console.log("equipment info ", data);
      this.timeStamp = moment(data[0].TimeStamp).format("hh:mm:ss A, dddd, Do MMMM YYYY");
      this.initFlag = true;
      this.firstLoad = false;
    });

  }

  private reload(){
    this.posInfoService.getShelfData(this.equipmentId).subscribe((data: any) => {
      this.equipmentData = data[0];
      console.log("equipment info ", data);
      this.timeStamp = moment(data[0].TimeStamp).format("hh:mm:ss A, dddd, Do MMMM YYYY");
    });
  }

}
