import { NgModule, Component, OnInit } from '@angular/core';
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule, FormGroup, FormArray, FormBuilder, FormsModule as AngularFormsModule } from "@angular/forms";
// import { SettingsInterface } from './settings.interface';

@Component({

  selector: 'settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})

export class SettingsComponent implements OnInit {
  public myForm: FormGroup;

      constructor(private _fb: FormBuilder) { }


  ngOnInit() {
    this.myForm = this._fb.group({
           name: [''],
           addresses: this._fb.array([
               this.initAddress(),
           ])
       });
  }
  initAddress() {
       return this._fb.group({
           street: [''],
           postcode: ['']
       });
   }
  addAddress() {
          const control = <FormArray>this.myForm.controls['addresses'];
          control.push(this.initAddress());
      }

      removeAddress(i: number) {
          const control = <FormArray>this.myForm.controls['addresses'];
          control.removeAt(i);
      }

      // save(model: SettingsInterface) {
      //     // call API to save
      //     // ...
      //     console.log(model);
      // }

    private dismissBaCard(index: number) {
        console.log("dismiss ba card", index);
    }
}
