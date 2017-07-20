import { Component, OnInit, AfterViewInit, Input } from "@angular/core";
import { PosListService } from "./pos-list.service";
import { Subscription } from "rxjs";
import { Observable } from "rxjs/Observable";
import "rxjs/Rx";
import { 
  IADTableOptions, 
  IMColumn, 
  IMData, 
  IMTableState, 
  MColumnDataType,
  MTableType
} from "../../../shared/controls/adtable/adtable.models";

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

  private posTable: IADTableOptions = {
    columns: [
      {
        title: "POS Name",
        name: "PointOfSaleName",
        dataType: MColumnDataType.string,
        filterable: true,
        sortable: true
      },
      {
        title: "POS Address",
        name: "PointOfSaleAddress",
        dataType: MColumnDataType.string,
        filterable: true,
        sortable: false
      },
      {
        title: "POS Telephone",
        name: "PointOfSaleTelephone",
        dataType: MColumnDataType.string,
        filterable: true,
        sortable: false
      },
      {
        title: "Contact Person Name",
        name: "ContactPersonName",
        dataType: MColumnDataType.string,
        filterable: true,
        sortable: false
      }
      
    ],
    load: (tableState: IMTableState) => this.getTableData(),
    dataTypeName: "Points of Sale",
    tableType: MTableType.details,
    pagination: false,
    filterable: true,
    sortable: true,
    isDataLoading: true,
    pageData: {
      currentPage: 1,
      itemsPerPage: 10
    },
    rowLink: "pos-info"
  };

  ngOnInit() {
    this.getData();
  }

  ngAfterViewInit() {
    this.initFlag = true;
  }

  private getData() {
    this.posListService.getPointsOfSales().subscribe((data: any[]) => {
      console.log("posListService ", data);
      this.tableData = data;
    });
  }

  private getTableData(): Observable<IMData> {
    return this.posListService.getPOSTableData().do((i): any => {
      this.posTable.isDataLoading = false;
    });
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
