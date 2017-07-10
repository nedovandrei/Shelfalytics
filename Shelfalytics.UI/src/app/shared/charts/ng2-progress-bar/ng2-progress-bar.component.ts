import {Component, OnInit, Input} from "@angular/core";

@Component({
    selector: "ng2-progress-bar",
    templateUrl: 'ng2-progress-bar.component.html',
})
export class Ng2ProgressBar implements OnInit {
  @Input() value: number;
  @Input() max: number;

  private viewValue: number;
  ngOnInit() {
    this.viewValue = Math.floor(this.value);
  }
}
