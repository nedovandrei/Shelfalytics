import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { global } from "../../global"

@Injectable()
export class ExceptionsService {
    constructor(private http: HttpClient) {}

    // private header: HttpHeaders = new HttpHeaders().delete("Access-Control-Allow-Origin");

    getExceptions(){
        return this.http.get(`${global.apiPath}ExceptionLog/all`);
    }

    getFilteredExceptions(filter: any){
        return this.http.post(`${global.apiPath}ExceptionLog/filtered`, filter);
    }

    getById(id: number){
        return this.http.get(`${global.apiPath}ExceptionLog/byId?exceptionId=${id}`);
    }

    deleteLog(id: number){
        return this.http.delete(`${global.apiPath}ExceptionLog?exceptionId=${id}`);
    }

    deleteAll(){
        return this.http.get(`${global.apiPath}ExceptionLog/deleteAll`);
    }
}