import { Injectable } from "@angular/core";
import { global, FilterFactory } from "../../global";
import { AjaxService } from "../../shared/services/ajax.service";
import { Observable } from "rxjs/Observable";
import { GlobalFilter } from "../../shared/services/global-filter.service";

@Injectable()
export class MainService {
    constructor(private ajaxService: AjaxService, private globalFilter: GlobalFilter) { }

    getTopSkuOos(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/topSkuOOS`, new FilterFactory(this.globalFilter, filter));
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/topSkuOOS`, new FilterFactory(this.globalFilter));
        }
    }

    getSalesSummary(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/productSalesSummary`, new FilterFactory(this.globalFilter, filter));
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/productSalesSummary`, new FilterFactory(this.globalFilter));
        }
    }

    getTopPosInOos(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/posOOSsummary`, new FilterFactory(this.globalFilter, filter));
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/posOOSsummary`, new FilterFactory(this.globalFilter));
        }
    }

    getLossesDueToOosSummary(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/lossesSummary`, new FilterFactory(this.globalFilter, filter));
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/lossesSummary`, new FilterFactory(this.globalFilter));
        }
    }

    getTopBestBusinessDevelopers(filter?: any) {
        if (filter) {
            return this.ajaxService.post(`${global.apiPath}Statistics/topBestBusinessDevelopers`, new FilterFactory(this.globalFilter, filter));
        } else {
            return this.ajaxService.post(`${global.apiPath}Statistics/topBestBusinessDevelopers`, new FilterFactory(this.globalFilter));
        }
    }
}
