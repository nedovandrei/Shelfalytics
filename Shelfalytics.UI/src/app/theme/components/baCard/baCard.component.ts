import {Component, Input} from '@angular/core';

@Component({
  selector: 'ba-card',
  templateUrl: './baCard.html',
  styleUrls: ["./baCard.scss"]
})
export class BaCard {
  @Input() title:String;
  @Input() navTabs:String;
  @Input() baCardClass:String;
  @Input() cardType:String;
}
