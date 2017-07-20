import { Component } from "@angular/core";
import { FormGroup, AbstractControl, FormBuilder, Validators } from "@angular/forms";
import { AuthService, Credentials } from "../../auth/auth.service";
import { Observable } from "rxJs/Observable";

@Component({
  selector: "login",
  templateUrl: "./login.html",
  styleUrls: ["./login.scss"],
  providers: [AuthService]
})
export class Login {

  private form: FormGroup;
  private username: AbstractControl;
  private password: AbstractControl;
  private submitted: boolean = false;

  private invalidData: boolean = false;

  constructor(fb:FormBuilder, private auth: AuthService) {
    this.form = fb.group({
      'username': ['', Validators.compose([Validators.required, Validators.minLength(4)])],
      'password': ['', Validators.compose([Validators.required, Validators.minLength(4)])]
    });

    this.username = this.form.controls['username'];
    this.password = this.form.controls['password'];
  }

  onSubmit(values: Credentials): void {
    this.submitted = true;
    if (this.form.valid) {
      // your code goes here
      console.log(values);
      values.grant_type = "password";
      this.auth.login(values).subscribe(
        // We're assuming the response will be an object
        // with the JWT on an id_token key
        data => { 
          this.invalidData = false;
          console.log("login data", data);
          localStorage.setItem("token", data.access_token);
          localStorage.setItem("token_type", data.token_type);
          localStorage.setItem("expires_in", data.expires_in);
            
        },
        error => {
          this.invalidData = true;
          console.log(error);
        }
      );

    }
  }
}
