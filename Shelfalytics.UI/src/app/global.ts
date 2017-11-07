import { Subject } from "rxjs/Subject";
import { Injectable } from "@angular/core";
import { GlobalFilter } from "./shared/services/global-filter.service";

export const global = {
    // apiPath: "http://localhost:6070/api/",
    // apiPath: "http://ithell-001-site1.ctempurl.com/api/", // DEV SERVER
    // apiPath: "http://shelfalytics-001-site2.gtempurl.com/api/", // UAT SERVER
    apiPath: "http://api.shelf.work/api/", // UAT SERVER 2
    imagePath: "http://api.shelf.work/Content/photos/",
    googleMapsApiKey: "AIzaSyDAVKaScBsoz10FfT-hobo1IMHYp-zssvg"
};

@Injectable()
export class Language {
    currentLanguage: string;
    onLanguageChange: Subject<string> = new Subject<string>();
}

export class FilterFactory {
    StartTime: any;
    EndTime: any;
    ClientId: number;
    IsAdmin: boolean;
    Role: number;
    GeneralManagerId: number;
    SupervisorId: number;

    constructor(globalFilter: GlobalFilter, filter?: any) {
        this.StartTime = filter ? filter.StartTime : globalFilter.startDate;
        this.EndTime = filter ? filter.EndTime : globalFilter.endDate;
        this.ClientId = globalFilter.clientId;
        this.IsAdmin = globalFilter.isAdmin;
        this.Role = globalFilter.role;
        this.GeneralManagerId = globalFilter.generalManagerId;
        this.SupervisorId = globalFilter.supervisorId;
    }
}
