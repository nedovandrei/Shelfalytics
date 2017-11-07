import { Component, OnInit, OnDestroy } from '@angular/core';
import { StatisticsService } from "./statistics.service";
import { GlobalFilter } from "../../shared/services/global-filter.service";
import { Language } from "../../global";
import {saveAs as importedSaveAs} from "file-saver";
import "rxjs/Rx";
import * as _ from "underscore";
import * as moment from "moment";

@Component({
  selector: 'statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.scss'],
  providers: [StatisticsService]
})
export class StatisticsComponent implements OnInit, OnDestroy {

  constructor(private statisticsService: StatisticsService, private globalFilter: GlobalFilter, private lang: Language) { }

  private exportFilter = {
    StartTime: this.globalFilter.startDate,
    EndTime: this.globalFilter.endDate,
    ClientId: this.globalFilter.clientId,
    IsAdmin: this.globalFilter.isAdmin,
    PointsOfSale: [],
    Equipments: [],
    Products: [],
    TradeChannels: [],
    ChainNames: [],
    Cities: [],
    Locale: this.lang.currentLanguage
  }

  private selectsData: any;
  private initSelects: boolean = false;
  private exportOngoing: boolean = false;


  ngOnInit() {

    this.lang.onLanguageChange.subscribe(() => {
      this.exportFilter.Locale = this.lang.currentLanguage;
    });
    this.globalFilter.onDateRangeChanged.subscribe(() => {
      this.exportFilter.StartTime = this.globalFilter.startDate;
      this.exportFilter.EndTime = this.globalFilter.endDate;
    })
    this.globalFilter.globalStateSubject.next(true);
    this.globalFilter.globalStateValueSubject.next(true);

    this.loadSelects();
  }

  private loadSelects() {
    this.statisticsService.getFilterSelects().subscribe((data: any) => {
      this.selectsData = data;

      this.selectsData.RetailChain = _.uniq(_.map(data.PointsOfSale, (item: any) => {
        return item.ChainName;
      }), false);
      this.initSelects = true;
      // console.log("selects data", data);
    });
  }

  private exportExcel() {
    this.exportOngoing = true;
    // console.log("export excel filter", this.exportFilter);
    this.statisticsService.exportExcel(this.exportFilter).subscribe((data: any) => {
      importedSaveAs(data, `export${this.exportFilter.StartTime.format("DD.MM.YYYY")}-${this.exportFilter.EndTime.format("DD.MM.YYYY")}.xlsx`);

      this.exportOngoing = false;
    })
  }

  private dateSelectChanged(newDate: any) {
    switch(newDate.srcElement.value) {
      case "0":
        this.globalFilter.globalStateValueSubject.next(true);
        this.exportFilter.StartTime = this.globalFilter.startDate;
        this.exportFilter.EndTime = this.globalFilter.endDate;
        break;

      case "1":
        this.exportFilter.StartTime = moment().utc(true).subtract(7, "day");
        this.exportFilter.EndTime = moment().utc(true);
        this.globalFilter.globalStateValueSubject.next(false);
        break;

      case "2":
        this.exportFilter.StartTime = moment().utc(true).subtract(1, "month");
        this.exportFilter.EndTime = moment().utc(true);
        this.globalFilter.globalStateValueSubject.next(false);
        break;

      case "3":
        this.exportFilter.StartTime = moment().utc(true).subtract(6, "month");
        this.exportFilter.EndTime = moment().utc(true);
        this.globalFilter.globalStateValueSubject.next(false);
        break;

      case "4":
        this.exportFilter.StartTime = moment().utc(true).subtract(1, "year");
        this.exportFilter.EndTime = moment().utc(true);
        this.globalFilter.globalStateValueSubject.next(false);
        break;
    }
  }

  localChanged(event: any) {
    this.exportFilter.Cities = [];
    this.exportFilter.Cities.push(event.srcElement.value);
  }

  tradeChanelChanged(event: any) {
    this.exportFilter.TradeChannels = [];
    this.exportFilter.TradeChannels.push(event.srcElement.value);
  }

  retailChainChanged(event: any) {
    this.exportFilter.ChainNames = [];
    this.exportFilter.ChainNames.push(event.srcElement.value);
  }

  pointOfSaleChanged(event: any) {
    this.exportFilter.PointsOfSale = [];
    this.exportFilter.PointsOfSale.push(event.srcElement.value);
  }

  equipmentChanged(event: any) {
    this.exportFilter.Equipments = [];
    this.exportFilter.Equipments.push(event.srcElement.value);
  }

  productChanged(event: any) {
    this.exportFilter.Products = [];
    this.exportFilter.Products.push(event.srcElement.value);
  }

  ngOnDestroy() {
    this.globalFilter.globalStateSubject.next(false);
  }
}
