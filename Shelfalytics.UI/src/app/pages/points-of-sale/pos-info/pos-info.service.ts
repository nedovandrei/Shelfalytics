import { Injectable } from "@angular/core";
import { global } from "../../../global";
import { AjaxService } from "../../../shared/services/ajax.service";
import { Observable } from "rxjs/Observable";
import { GlobalFilter } from "../../../shared/services/global-filter.service";
import * as moment from "moment";

@Injectable()
export class PosInfoService {

  constructor(private ajaxService: AjaxService, private globalFilter: GlobalFilter) { }

  getShelfData(equipmentId: number) {
    return this.ajaxService.get(`${global.apiPath}EquipmentData`, { "equipmentId": equipmentId });
    
  }

  getPointOfSaleData(id: number) {
    return this.ajaxService.get(global.apiPath + "PointOfSale", { posId: id });
  }

  getEquipmentOOSPercentage(equipmentId: number) {
    return this.ajaxService.post(`${global.apiPath}Statistics/EquipmentOOS?equipmentId=${equipmentId}`, {
      StartTime: this.globalFilter.startDate,
      EndTime: this.globalFilter.endDate
    });
  }

  getPosOOSPercentage(posId: number) {
    return this.ajaxService.post(`${global.apiPath}Statistics/POSOOS?posId=${posId}`, {
      StartTime: this.globalFilter.startDate,
      EndTime: this.globalFilter.endDate
    });
  }

  getPOSSales(equipmentId: number) {
    return this.ajaxService.post(`${global.apiPath}Statistics/ProductSales?equipmentId=${equipmentId}`, {
      StartTime: this.globalFilter.startDate,
      EndTime: this.globalFilter.endDate
    });
  }

  getLossesDueToOOS(equipmentId: number) {
    return this.ajaxService.post(`${global.apiPath}Statistics/equipmentLosses?equipmentId=${equipmentId}`, {
      StartTime: this.globalFilter.startDate,
      EndTime: this.globalFilter.endDate
    });
  }

}
