import {Component, OnInit, Input} from "@angular/core";

@Component({
    selector: "ng2-progress-bar",
    templateUrl: 'ng2-progress-bar.component.html',
})
export class Ng2ProgressBar implements OnInit {
  @Input() value: number;
  @Input() max: number;
  @Input() result : string;
  private viewValue: number;
  ngOnInit() {
    this.viewValue = Math.floor(this.value);
    this.result = this.viewValue + "%";
  }
}
