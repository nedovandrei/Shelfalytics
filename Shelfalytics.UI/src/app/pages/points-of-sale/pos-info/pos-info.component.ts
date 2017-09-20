import { Component, OnInit, OnDestroy } from "@angular/core";
import { PosInfoService } from "./pos-info.service";
import { ActivatedRoute } from "@angular/router";
import { GlobalFilter } from "../../../shared/services/global-filter.service";
import * as moment from "moment";

@Component({
  selector: "pos-info",
  templateUrl: "./pos-info.component.html",
  styleUrls: ["./pos-info.component.scss"],
  providers: [PosInfoService]
})
export class PosInfoComponent implements OnInit, OnDestroy {

  constructor(
    private posInfoService: PosInfoService, 
    private route: ActivatedRoute, 
    private globalFilter: GlobalFilter
  ) { }

  private initFlag: boolean = false;
  private equipmentInitFlag: boolean = false;
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

  private equipmentOOSChartData = {};
  private POSOOSChartData = {};
  private equipmentActualFillChartData = {};
  private POSActualFillChartData = {};

  private equipmentProductOOSTable = [];
  private equipmentLossesDueToOOSTable = [];

  private salesChartData = {};

  ngOnInit() {
    this.paramSubscription = this.route.params.subscribe(params => {
       this.id = parseInt(params["id"]); // (+) converts string 'id' to a number

       this.loadData();
    });

    this.globalFilter.onDateRangeChanged.subscribe((dateTimeValue: any) => {
      console.log("fired onDateRangeChange event");
      this.loadData();
    });

    // this._testService.getShelfData().subscribe((data: any) => {
    //   this.equipmentData = data[0];
    //   this.initFlag = true;
    // });
  }

  private loadData() {
    this.initFlag = false;
    this.posInfoService.getPointOfSaleData(this.id).subscribe((data: any) => {
      this.posData = data[0];
      this.equipmentInFocus = this.posData.EquipmentIds[0];

      this.openHours = moment(this.posData.OpeningHours).format("hh:mm A");
      this.closeHours = moment(this.posData.ClosingHours).format("hh:mm A");

      this.posInfoService.getPosOOSPercentage(this.id).subscribe((oosData: any) => {
        this.POSOOSChartData = {
          data: oosData.TotalOOS,
          color: oosData.TotalOOS < 20 ? "rgba(255, 255, 255, 1)" :
              oosData.TotalOOS < 50 ? "rgba(223, 184, 28, 1)" : "rgba(232, 86, 86, 1)"
        };
        this.POSActualFillChartData = {
          data: oosData.ActualFill,
          color: oosData.ActualFill < 20 ? "rgba(255, 255, 255, 1)" :
              oosData.ActualFill < 50 ? "rgba(223, 184, 28, 1)" : "rgba(232, 86, 86, 1)"
        };
      });

      if (this.equipmentInFocus) {
        this.loadEquipmentData();
      } else {
        this.initFlag = true;
      }


    });
  }

  private loadEquipmentData() {
    this.equipmentInitFlag = false;
    this.posInfoService.getEquipmentOOSPercentage(this.equipmentInFocus).subscribe((oosData: any) => {
          // this.OOSChartOptions.series.push(percentage);
          // this.OOSChartOptions.series.push(100);
          this.equipmentProductOOSTable = oosData.OOSProducts;

          this.equipmentOOSChartData = {
            data: oosData.TotalOOS,
            color: oosData.TotalOOS < 20 ? "rgba(255, 255, 255, 1)" :
              oosData.TotalOOS < 50 ? "rgba(223, 184, 28, 1)" : "rgba(232, 86, 86, 1)"
          };

          this.equipmentActualFillChartData = {
            data: oosData.ActualFill,
            color: oosData.ActualFill < 20 ? "rgba(255, 255, 255, 1)" :
              oosData.ActualFill < 50 ? "rgba(223, 184, 28, 1)" : "rgba(232, 86, 86, 1)"
          };
          
          this.posInfoService.getPOSSales(this.equipmentInFocus).subscribe((saleData: any) => {
            this.salesChartData = {
              dataProvider: saleData,
              legendField: "ShortProductName",
              valueFields: ["Sales"]
            };
            this.equipmentInitFlag = true;
            this.initFlag = true;
          });

          this.posInfoService.getLossesDueToOOS(this.equipmentInFocus).subscribe((lossesData: any) => {
            this.equipmentLossesDueToOOSTable = lossesData;
          });
    });  
  }

  private changeEquipmentInFocus(equipmentId: number) {
    this.equipmentInFocus = equipmentId;
    this.loadEquipmentData();
  }

  ngOnDestroy() {
    this.paramSubscription.unsubscribe();
  }
  
}
