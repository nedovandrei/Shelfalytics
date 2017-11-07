import { Component } from "@angular/core";
import { FormGroup, AbstractControl, FormBuilder, Validators } from "@angular/forms";
import { AuthService, Credentials } from "../../auth/auth.service";
import { Observable } from "rxJs/Observable";
import { JwtHelper } from "angular2-jwt";
import { Router } from "@angular/router";
import { GlobalFilter } from "../../shared/services/global-filter.service";

@Component({
  selector: "login",
  templateUrl: "./login.html",
  styleUrls: ["./login.scss"],
  providers: [AuthService, JwtHelper]
})
export class Login {

  private form: FormGroup;
  private username: AbstractControl;
  private password: AbstractControl;
  private submitted: boolean = false;

  private invalidData: boolean = false;

  private loadingFlag: boolean = false;

  constructor(
    fb: FormBuilder, 
    private auth: AuthService, 
    private jwt: JwtHelper, 
    private router: Router, 
    private filter: GlobalFilter
  ) {
    this.form = fb.group({
      "username": ["", Validators.compose([Validators.required, Validators.minLength(4)])],
      "password": ["", Validators.compose([Validators.required, Validators.minLength(4)])]
    });

    this.username = this.form.controls["username"];
    this.password = this.form.controls["password"];
  }

  onSubmit(values: Credentials): void {
    this.submitted = true;
    this.loadingFlag = true;
    if (this.form.valid) {
      // your code goes here
      // console.log(values);
      values.grant_type = "password";
      this.auth.login(values).subscribe(
        // We're assuming the response will be an object
        // with the JWT on an id_token key
        data => { 
          this.invalidData = false;
          // console.log("login data", data);
          // const decodedJwt = this.jwt.decodeToken(data.access_token);
          localStorage.setItem("token", data.access_token);
          
          // localStorage.setItem("token_type", data.token_type);
          // localStorage.setItem("expires_in", data.expires_in);
          // localStorage.setItem("loggedUser", this.jwt.decodeToken(data.access_token));
          this.router.navigate(["/"]);
          this.filter.userLogged.next();
          this.loadingFlag = false;
        },
        error => {
          this.loadingFlag = false;
          this.invalidData = true;
          // console.log(error);
        }
      );

    }
  }
}
