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
import { IGoogleMapsData, ICoordinates, IMarker } from "../../../shared/maps/google-maps/google-maps.model";
import * as _ from "underscore";

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
  private mapInit = false;

  private posTable: IADTableOptions = {
    columns: [
      {
        title: "pointsOfSale.posTable.posName",
        name: "PointOfSaleName",
        dataType: MColumnDataType.string,
        filterable: true,
        sortable: true
      },
      {
        title: "pointsOfSale.posTable.posAddress",
        name: "PointOfSaleAddress",
        dataType: MColumnDataType.string,
        filterable: true,
        sortable: false
      },
      {
        title: "pointsOfSale.posTable.posTelephone",
        name: "PointOfSaleTelephone",
        dataType: MColumnDataType.string,
        filterable: true,
        sortable: false
      },
      {
        title: "pointsOfSale.posTable.contactPersonName",
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

  private mapCardTitle: string = "'pointsOfSale.posOnMap'";

  
  private mapSettings: IGoogleMapsData = {
    center: {
      lat: 0,
      lng: 0
    },
    // zoom: 16,
    markers: [],
    zoomControl: true
  };

  ngOnInit() {
    this.getData();
  }

  ngAfterViewInit() {
    this.initFlag = true;
  }

  private getData() {
    this.posListService.getPointsOfSales().subscribe((data: any[]) => {

      this.tableData = data;

      for (let i = 0; i < data.length; i++) {

        const marker: IMarker = {
          coordinates: {
            lat: data[i].Latitude,
            lng: data[i].Longitude
          },
          title: data[i].PointOfSaleName,
          markerDraggable: false,
          label: data[i].PointOfSaleName.slice(0, 1),
          opacity: 1,
          visible: true,
          infoWindowContent: `${data[i].PointOfSaleName}, ${data[i].PointOfSaleAddress}`,
          linkUrl: "pos-info",
          linkUrlId: data[i].PointOfSaleId
        };

        this.mapSettings.markers.push(marker);
      }

      this.mapSettings.center.lat = _.reduce(this.mapSettings.markers, (memo: number, item: any) => {
        return memo + item.coordinates.lat;
      }, 0) / (this.mapSettings.markers.length === 0 ? 1 : this.mapSettings.markers.length);

      this.mapSettings.center.lng = _.reduce(this.mapSettings.markers, (memo: number, item: any) => {
        return memo + item.coordinates.lng;
      }, 0) / (this.mapSettings.markers.length === 0 ? 1 : this.mapSettings.markers.length);

      console.log("mapSettings", this.mapSettings);

      this.mapInit = true;
    });
  }

  private getTableData(): Observable<IMData> {
    return this.posListService.getPOSTableData().do((i): any => {
      this.posTable.isDataLoading = false;
    });
  }
}
