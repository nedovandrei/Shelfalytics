import { Injectable } from "@angular/core";
import { Http, RequestOptionsArgs, Headers } from "@angular/http";
import "rxjs/add/operator/map";
import { Router } from "@angular/router";
import { tokenNotExpired, JwtHelper } from "angular2-jwt";
import { GlobalFilter } from "../shared/services/global-filter.service";
import { global } from "../global";

@Injectable()
export class AuthService {

  constructor(private http: Http, private router: Router, private jwt: JwtHelper, private filter: GlobalFilter) {}

  login(credentials: Credentials) {
    //   console.log("credentials", credentials);
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
        res => {
            return res.json();
        }
    );
  }

  isLoggedIn() {
      return tokenNotExpired();
  }

  logout() {
      localStorage.removeItem("token");
      this.router.navigate(["/login"]);
  }

  getUserData() {
      const token = localStorage.getItem("token");
      if (token) {
          return this.jwt.decodeToken(token);
      } else {
          this.router.navigate(["/login"]);
      }
  }
}

export interface Credentials {
    grant_type: string;
    username: string;
    password: string;
}

