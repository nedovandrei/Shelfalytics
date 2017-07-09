import { Injectable } from '@angular/core';
import { global } from '../../../global';
import { AjaxService } from '../../../shared/services/ajax.service';
import { Observable } from 'rxjs/Observable';
import * as moment from 'moment';

@Injectable()
export class PosInfoService {

  constructor(private ajaxService: AjaxService) { }

  getShelfData(equipmentId: number) {
    return this.ajaxService.get(`${global.apiPath}EquipmentData`, { 'equipmentId': equipmentId });
    
  }

  getPointOfSaleData() {
    return this.ajaxService.get(global.apiPath + 'PointOfSale', { posId: 1 });
  }

  getOOSPercentage(equipmentId: number) {
    return this.ajaxService.post(global.apiPath + 'Statistics?equipmentId=' + equipmentId, {
      StartTime: moment().subtract(2, 'months'),
      EndTime: moment(),
    });
  }

}
