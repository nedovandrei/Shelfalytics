import { Injectable } from "@angular/core";
import { global, FilterFactory } from "../../../global";
import { AjaxService } from "../../../shared/services/ajax.service";
import { Observable } from "rxjs/Observable";
import { GlobalFilter } from "../../../shared/services/global-filter.service";
// import "rxjs/add/operator/map";
// import "rxjs/add/operator/catch";
// import "rxjs/add/bservable/of";
import { IMData } from "../../../shared/controls/adtable/adtable.models";

@Injectable()
export class PosListService {
    constructor(private ajaxService: AjaxService, private globalFilter: GlobalFilter) {}

    getPointsOfSales() {
        return this.ajaxService.post(`${global.apiPath}PointOfSale/GetPointsOfSales`,  new FilterFactory(this.globalFilter));
    }

    getPOSTableData() {
        return this.ajaxService.post(`${global.apiPath}PointOfSale/GetPointsOfSales`,  new FilterFactory(this.globalFilter)).map((data: any) => {
            const rows: IMData = {
                rows: data,
                totalRows: data.length
            };
            return rows;
        });
    }
}
