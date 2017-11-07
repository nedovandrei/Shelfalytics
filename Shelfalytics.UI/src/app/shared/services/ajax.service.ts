import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";
import { RequestOptions, ResponseContentType, Http, Headers } from "@angular/http";
// import { JwtHelper } from "angular2-jwt";

@Injectable()
export class AjaxService {
    constructor(private router: Router, private http: Http) {}
    get<T>(url: string, urlParams?: any): Observable<T> {
        const token = localStorage.getItem("token");

        if (!token) {
            this.redirectToLogin();
        }

        return Observable.create((subscriber: any) => {
            const xhr = new XMLHttpRequest();
            xhr.overrideMimeType("application/json");
            xhr.onload = () => {
                subscriber.next(JSON.parse(xhr.responseText));
                subscriber.complete();
            };
            xhr.open("GET", url + this.formatUrlParams(urlParams), true);
            xhr.setRequestHeader("Authorization", `Bearer ${token}`);
            xhr.onreadystatechange = () => {
                if (xhr.readyState === 4) {
                    if (xhr.readyState === 4) {
                        if (xhr.status === 200) {
                            subscriber.next(JSON.parse(xhr.responseText));
                            subscriber.complete(); 
                        } else if (xhr.status === 401) {
                            this.router.navigate(["/login"]);
                            subscriber.error(xhr);
                        } else {
                            subscriber.error(xhr);
                        }
                    }
                }
            };
            xhr.send();
        });
    }

    post<T>(url: string, data: any): Observable<T> {
        const token = localStorage.getItem("token");

        if (!token) {
            this.redirectToLogin();
        }

        return Observable.create((subscriber: any) => {
            const xhr = new XMLHttpRequest();
            
            xhr.open("POST", url, true);
            xhr.setRequestHeader("Authorization", `Bearer ${token}`);
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.onreadystatechange = () => {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        subscriber.next(JSON.parse(xhr.responseText));
                        subscriber.complete(); 
                    } else if (xhr.status === 401) {
                        this.redirectToLogin();
                        // subscriber.error(xhr);
                    } else {
                        subscriber.error(xhr);
                    }
                }
            };
            xhr.send(JSON.stringify(data));
        });
    }

    filePost(url: string, data: any): Observable<Blob> {
        const token = localStorage.getItem("token");

        if (!token) {
            this.redirectToLogin();
        }

        // return Observable.create((subscriber: any) => {
        //     const xhr = new XMLHttpRequest();
            
        //     xhr.open("POST", url, true);
        //     xhr.setRequestHeader("Authorization", `Bearer ${token}`);
        //     xhr.setRequestHeader("Content-type", "application/json");
        //     xhr.onreadystatechange = () => {
        //         if (xhr.readyState === 4) {
        //             if (xhr.status === 200) {
        //                 subscriber.next(xhr.response.blob());
        //                 subscriber.complete(); 
        //             } else if (xhr.status === 401) {
        //                 this.redirectToLogin();
        //                 // subscriber.error(xhr);
        //             } else {
        //                 subscriber.error(xhr);
        //             }
        //         }
        //     };
        //     xhr.send(JSON.stringify(data));
        // });
        let options = new RequestOptions({responseType: ResponseContentType.Blob, headers: new Headers({"Authorization": `Bearer ${token}`}) });
        return this.http.post(url, data, options)
            .map(res => res.blob());
            // .catch(console.log("error"));
    }

    private redirectToLogin() {
        this.router.navigate(["/login"]);
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
