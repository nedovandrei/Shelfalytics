import { Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { PosListService } from "./pos-list.service";

// @Component({
//   selector: "equipment-count-view",
//   template: "<span>{{renderValue.length}}</span>"
// })
// export class EquipmentCountViewComponent implements OnInit {
//   @Input() value: any;

//   private renderValue: any;
//   ngOnInit() {
//     console.log("ulalala");
//     this.renderValue = this.value;
//   }

// }


@Component({
  selector: "pos-list",
  templateUrl: "./pos-list.component.html",
  styleUrls: ["./pos-list.component.scss"],
  providers: [PosListService],
})
export class PosListComponent implements OnInit, AfterViewInit {

  constructor(private posListService: PosListService) { }

  private tableData: any[];
  private initFlag = false;

  ngOnInit() {
    this.posListService.getPointsOfSales().subscribe((data: any[]) => {
      console.log("posListService ", data);
      this.tableData = data;
    });
  }

  ngAfterViewInit() {
    this.initFlag = true;
  }

  private tableColumns = {
    PointOfSaleName: {
      title: "POS Name",
      type: "string"
    },
    PointOfSaleAddress: {
      title: "POS Address",
      type: "string",
    },
    PointOfSaleTelephone: {
      title: "POS Telephone",
      type: "string"
    },
    ContactPersonName: {
      title: "Comtact Person Name",
      type: "string"
    },
    // Equipment: {
    //   title: "Equipment Count",
    //   type: "string",
    //   renderComponent: EquipmentCountViewComponent,
    //   onComponentInitFunction(instance) {

    //   }
    // }
  };

  
}
