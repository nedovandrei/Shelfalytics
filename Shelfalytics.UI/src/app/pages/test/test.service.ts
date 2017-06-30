import { Injectable } from '@angular/core';
import { global } from "../../global";
import { AjaxService } from "../../shared/services/ajax.service";
import { Observable } from "rxjs/Observable";

@Injectable()
export class TestService {

  constructor(private ajaxService: AjaxService) { }

  getShelfData(equipmentId: number){
    return this.ajaxService.get(global.apiPath + "EquipmentData", { equipmentId: equipmentId});
    
  }

  getPointOfSaleData(){
      return this.ajaxService.get(global.apiPath + "PointOfSale", { posId: 1 });
  }



}
