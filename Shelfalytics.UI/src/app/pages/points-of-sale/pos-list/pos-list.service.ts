import { Injectable } from '@angular/core';
import { global } from '../../../global';
import { AjaxService } from "../../../shared/services/ajax.service";
import { Observable } from "rxjs/Observable";
// import "rxjs/add/operator/map";
// import "rxjs/add/operator/catch";
// import "rxjs/add/bservable/of";
import { IMData } from "../../../shared/controls/adtable/adtable.models";

@Injectable()
export class PosListService {
    constructor(private ajaxService: AjaxService) {}

    getPointsOfSales() {
        return this.ajaxService.get(`${global.apiPath}PointOfSale/GetPointsOfSales`);
    }

    getPOSTableData() {
        return this.ajaxService.get(`${global.apiPath}PointOfSale/GetPointsOfSales`).map((data: any) => {
            const rows: IMData = {
                rows: data,
                totalRows: data.length
            };
            return rows;
        });
    }
}
