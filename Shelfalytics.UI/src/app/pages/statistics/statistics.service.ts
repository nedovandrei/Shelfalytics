import { Injectable } from "@angular/core";
import { global, FilterFactory } from "../../global";
import { AjaxService } from "../../shared/services/ajax.service";
import { Observable } from "rxjs/Observable";
import { GlobalFilter } from "../../shared/services/global-filter.service";

@Injectable()
export class StatisticsService {
    constructor(private globalFilter: GlobalFilter, private ajaxServie: AjaxService) { }

    getFilterSelects() {
        return this.ajaxServie.post(`${global.apiPath}export/selects`, new FilterFactory(this.globalFilter));
    }

    exportExcel(exportFilter: any) {
        return this.ajaxServie.filePost(`${global.apiPath}export/excel`, exportFilter);
    }

}