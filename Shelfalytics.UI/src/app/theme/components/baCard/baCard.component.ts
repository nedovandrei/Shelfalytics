import { Component, Input } from "@angular/core";

@Component({
  selector: "ba-card",
  templateUrl: "./baCard.html",
  styleUrls: ["./baCard.scss"]
})
export class BaCard {
  @Input() title: String;
  @Input() navTabs: String;
  @Input() baCardClass: String;
  @Input() cardType: String;
  @Input() tabChangeCallback: (index: number) => void;

  private selectedTab: number = 0;

  private onTabChange(index) {
    this.selectedTab = index;
    if (this.tabChangeCallback) {
      this.tabChangeCallback(index);
    }
  }
}
