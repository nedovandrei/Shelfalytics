import { Injectable } from "@angular/core";
import { global } from "../../global";
import { AjaxService } from "../../shared/services/ajax.service";
import { Observable } from "rxjs/Observable";
import { GlobalFilter } from "../../shared/services/global-filter.service";

@Injectable()
export class MainService {
    constructor(private ajaxService: AjaxService, private globalFilter: GlobalFilter) { }

    getTopSkuOos(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/topSkuOOS`, filter);
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/topSkuOOS`, {
                StartTime: this.globalFilter.startDate,
                EndTime: this.globalFilter.endDate
            });
        }
    }

    getSalesSummary(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/productSalesSummary`, filter);
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/productSalesSummary`, {
                StartTime: this.globalFilter.startDate,
                EndTime: this.globalFilter.endDate
            });
        }
    }

    getTopPosInOos(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/posOOSsummary`, filter);
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/posOOSsummary`, {
                StartTime: this.globalFilter.startDate,
                EndTime: this.globalFilter.endDate
            });
        }
    }

    getLossesDueToOosSummary(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/lossesSummary`, filter);
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/lossesSummary`, {
                StartTime: this.globalFilter.startDate,
                EndTime: this.globalFilter.endDate
            });
        }
    }
}
