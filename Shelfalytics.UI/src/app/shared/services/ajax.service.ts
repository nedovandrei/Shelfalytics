import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class AjaxService {
    get<T>(url: string, urlParams?: any): Observable<T> {
        return Observable.create((subscriber: any) => {
            const xhr = new XMLHttpRequest();
            xhr.overrideMimeType("application/json");
            xhr.onload = () => {
                subscriber.next(JSON.parse(xhr.responseText));
                subscriber.complete();
            };
            xhr.open("GET", url + this.formatUrlParams(urlParams), true);
            xhr.send();
        });
    }

    post<T>(url: string, data: any): Observable<T> {
        return Observable.create((subscriber: any) => {
            const xhr = new XMLHttpRequest();
            xhr.open("POST", url, true);
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.onreadystatechange = () => {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        subscriber.next(JSON.parse(xhr.responseText));
                        subscriber.complete();
                    } else {
                        subscriber.error(xhr);
                    }
                }
            };
            xhr.send(JSON.stringify(data));
        });
    }

    private formatUrlParams(urlParams?: any): string {
        if (urlParams == null) {
            return "";
        }

        var keys = Object.keys(urlParams);
        if (keys.length === 0) {
            return "";
        }

        return "?" + keys.filter((key: string) => {
                return urlParams[key] != null && urlParams[key] !== "";
            }).map((key: string) => {
                return key + "=" + urlParams[key];
            })
            .join("&");
    }
}
