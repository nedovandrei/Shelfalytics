import { Injectable } from "@angular/core";
import { global, FilterFactory } from "../../../global";
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
    return this.ajaxService.get(global.apiPath + "PointOfSale", { posId: id, clientId: this.globalFilter.clientId });
  }

  getEquipmentOOSPercentage(equipmentId: number) {
    return this.ajaxService.post(`${global.apiPath}Statistics/EquipmentOOS?equipmentId=${equipmentId}`, new FilterFactory(this.globalFilter));
  }

  getPosOOSPercentage(posId: number) {
    return this.ajaxService.post(`${global.apiPath}Statistics/POSOOS?posId=${posId}`, new FilterFactory(this.globalFilter));
  }

  getPOSSales(equipmentId: number) {
    return this.ajaxService.post(`${global.apiPath}Statistics/ProductSales?equipmentId=${equipmentId}`, new FilterFactory(this.globalFilter));
  }

  getLossesDueToOOS(equipmentId: number) {
    return this.ajaxService.post(`${global.apiPath}Statistics/equipmentLosses?equipmentId=${equipmentId}`, new FilterFactory(this.globalFilter));
  }

}
