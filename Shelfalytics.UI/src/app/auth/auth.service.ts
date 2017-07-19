import { Injectable } from "@angular/core";
import { Http, RequestOptionsArgs, Headers } from "@angular/http";
import "rxjs/add/operator/map";

import { tokenNotExpired } from "angular2-jwt";

import { global } from "../global";

@Injectable()
export class AuthService {

  constructor(private http: Http) {}

  login(credentials: Credentials) {
      console.log("credentials", credentials);
    let header: Headers = new Headers();
    header.append("Content-Type", "application/x-www-form-urlencoded");
    let requestOptions: RequestOptionsArgs = {
        headers: header
    };
    return this.http.post(
        `${global.apiPath}token`,
        `username=${credentials.username}&password=${credentials.password}&grant_type=${credentials.grant_type}`, 
        requestOptions
    ).map(
        res => res.json()
    );
  }

  isLoggedIn() {
      return tokenNotExpired();
  }
}

export interface Credentials {
    grant_type: string;
    username: string;
    password: string;
}

