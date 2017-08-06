import { Component, OnInit, AfterViewInit, Input, Output } from "@angular/core";

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"]
})
export class MainComponent implements OnInit, AfterViewInit {

  constructor() { }


  private mapInit: boolean = false;
  private initFlag: boolean = false;


  private baCardTabs: any[] = [
    {
      title: "Daily"
    },
    {
      title: "Monthly"
    },
    {
      title: "Yearly"
    }
  ];

  private baCardTabChangeCallback: any;

  ngOnInit() {

  }

  ngAfterViewInit() {
    this.baCardTabChangeCallback = (index: number) => {
      console.log("ba card tab push, index", index);
      console.log("main component ba card", this.baCardTabs[index].title);
    };
    this.initFlag = true;
  }

}
