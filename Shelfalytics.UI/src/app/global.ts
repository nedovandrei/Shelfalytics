import { Subject } from "rxjs/Subject";
import { Injectable } from "@angular/core";

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
