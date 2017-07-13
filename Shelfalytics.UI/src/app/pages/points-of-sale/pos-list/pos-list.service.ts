import { Injectable } from '@angular/core';
import { global } from '../../../global';
import { AjaxService } from '../../../shared/services/ajax.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class PosListService {
    constructor(private ajaxService: AjaxService) {}

    getPointsOfSales() {
        return this.ajaxService.get(`${global.apiPath}PointOfSale/GetPointsOfSales`);
    }
}