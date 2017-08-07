import { Injectable } from "@angular/core";
import { global } from "../../global";
import { AjaxService } from "../../shared/services/ajax.service";
import { Observable } from "rxjs/Observable";
import { GlobalFilter } from "../../shared/services/global-filter.service";

@Injectable()
export class MainService {
    constructor(private ajaxService: AjaxService, private globalFilter: GlobalFilter) { }

    getTopSkuOos() {
        return this.ajaxService.post(`${global.apiPath}Statistics/topSkuOOS`, {
            StartTime: this.globalFilter.startDate,
            EndTime: this.globalFilter.endDate
        });
    }

    getSalesSummary() {
        return this.ajaxService.post(`${global.apiPath}Statistics/productSalesSummary`, {
            StartTime: this.globalFilter.startDate,
            EndTime: this.globalFilter.endDate
        });
    }

    getTopPosInOos() {
        return this.ajaxService.post(`${global.apiPath}Statistics/posOOSsummary`, {
            StartTime: this.globalFilter.startDate,
            EndTime: this.globalFilter.endDate
        });
    }
}
