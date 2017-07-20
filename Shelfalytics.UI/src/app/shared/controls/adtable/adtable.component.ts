import { 
    Component, 
    OnInit, 
    Input, 
    ViewEncapsulation, 
    OnChanges, 
    SimpleChanges, 
    ChangeDetectionStrategy
} from "@angular/core";
import { CurrencyPipe, DecimalPipe, DatePipe } from "@angular/common";
import { DomSanitizer } from "@angular/platform-browser";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import "rxjs/add/operator/debounceTime";
import {
    IADTableOptions, IMTableState, IMData, IMColumn, MSortDirection, IMPageData, MColumnDataType, MColumnRenderType,
    MTableLoadType, MTableType, MTableLoadState
} from "./adtable.models";

@Component({
    selector: "adtable",
    templateUrl: "adtable.component.html",
    encapsulation: ViewEncapsulation.None,
    styleUrls: ["adtable.component.scss"],
    providers: [CurrencyPipe, DecimalPipe, DatePipe]
})
export class ADTableComponent implements OnInit {

    constructor(private sanitizer: DomSanitizer,
        private currencyPipe: CurrencyPipe,
        private decimalPipe: DecimalPipe,
        private datePipe: DatePipe) { }

    @Input() moptions: IADTableOptions;

    private columns: MColumn[] = [];
    private data: IMData = { rows: [], totalRows: 0 };
    private cachedData: IMData = { rows: [], totalRows: 0 };
    private tableState: IMTableState = { data: this.data, columns: [] };
    private pageData: MPageData;
    private isFilterVisible = false;
    private isDataLoaded = false;
    private eventTriggerDebounceTime = 500;
    private loadType = MTableLoadType.ajax;
    private displayedItemFirstIndex = 0;
    private displayedItemLastIndex = 0;
    private loadStates = MTableLoadState;
    private loadState = MTableLoadState.pending;

    private totalRecordCounter = 0;
    private rowsOnPage = 0;
    private showHeadingWithFilter = false;

    private get totalPages(): number {
        if (!this.moptions.pagination || this.cachedData.totalRows === null || this.tableState.pageData === null) {
            return 0;
        }

        return Math.ceil(this.cachedData.totalRows / this.tableState.pageData.itemsPerPage);
    }

    ngOnInit(): void {
        if (this.moptions === null) {
            throw new Error("Missing table options");
        }
        if (!this.moptions.columns || this.moptions.columns.length === 0) {
            throw new Error("Table options should contains at least one column configuration");
        }
        // if pagination is set true, is required to have sortable enabled
        if (this.moptions.pagination && !this.moptions.sortable) {
            throw new Error("Please set sortable to true, pagination cannot be used whitout sorting.");
        }

        // set load type
        if (this.moptions.loadType) {
            this.loadType = this.moptions.loadType;
        }

        this.moptions.refresh = new Subject<any>();
        this.moptions.refresh.subscribe(() => { this.loadData(); });

        // set columns configuration
        this.tableState.columns = this.columns = this.moptions.columns.map((col: IMColumn) => {
            const column: MColumn = new MColumn(col, this.moptions, this.currencyPipe, this.decimalPipe, this.datePipe);
            if (column.filterable) {
                this.setFilterChangeDebounce(column);
            }
            return column;
        });
        this.setColumnsWidth();

        if (this.moptions.sortable) {
            this.setDefaultSortDirection();
        }

        // set pagination 
        if (this.moptions.pagination) {
            this.tableState.pageData = this.pageData = new MPageData(this.moptions.pageData);
            this.setPageChangeDebounce();
        } else {
            this.pageData = new MPageData(this.moptions.pageData);
        }

        this.showHeadingWithFilter = this.moptions.tableType === MTableType.details;

        this.loadData();
    }

    private comparer(obj1: any, obj2: any, column: IMColumn): number {
        const item1 = obj1[column.name];
        const item2 = obj2[column.name];

        let direction = 0;
        if (item1 < item2) {
            direction = -1;
        }
        if (item1 > item2) {
            direction = 1;
        }
        if (column.sortDirection === MSortDirection.desc) {
            direction *= -1;
        }
        return direction;
    }

    private doInternalOperations() {
        let rows = this.cachedData.rows;
        this.cachedData.totalRows = this.cachedData.rows.length;
        // sorting
        const column = this.columns.find((col: IMColumn) => col.sortDirection !== null);
        rows.sort((obj1: any, obj2: any) => this.comparer(obj1, obj2, column));

        // pagination
        if (this.moptions.pagination) {
            const skip = this.pageData.itemsPerPage * (this.pageData.currentPage - 1);
            let take = skip + this.pageData.itemsPerPage;
            if (take > rows.length) {
                take = rows.length;
            }
            rows = rows.slice(skip, take);

            this.totalRecordCounter = this.cachedData.rows.length;
            this.rowsOnPage = this.pageData.itemsPerPage;

            this.displayedItemFirstIndex = skip + 1;
            this.displayedItemLastIndex = take;
        }

        this.data.rows = rows;
    }

    private loadData(isInternalTrigger: boolean = false): void {
        this.loadState = MTableLoadState.pending;
        this.isDataLoaded = false;

        if (this.loadType === MTableLoadType.local && isInternalTrigger) {
            this.loadDataSuccess(this.cachedData);
            return;
        }

        const data = this.moptions.load(this.tableState);
        if (data instanceof Observable) {
            const self = this;
            (data as Observable<IMData>).subscribe((mdata: IMData) => {
                this.loadDataSuccess.call(self, mdata);
            });
        } else {
            this.loadDataSuccess(data as IMData);
        }
    }

    private loadDataSuccess(mdata: IMData): void {
        console.log("loadDataSuccess", mdata);

        if (mdata === undefined) {
            this.loadState = MTableLoadState.fail;
            // this.serverErrorToast();
        } else {
            this.loadState = MTableLoadState.success;
        }


        if (!!mdata) {
            this.cachedData = mdata;
        }


        if (this.loadType === MTableLoadType.local) {
            this.doInternalOperations();

            const total = (mdata.totalRows) ? mdata.totalRows : mdata.rows.length;
            this.totalRecordCounter = total;
            // this.displayedItemFirstIndex = 1;
            // this.displayedItemLastIndex = total;

        } else {
            this.data = this.cachedData;
            if (mdata) {

                let total = (mdata.totalRows) ? mdata.totalRows : mdata.rows.length;
                this.totalRecordCounter = total;
                this.rowsOnPage = mdata.rows.length;

                this.displayedItemFirstIndex = this.pageData.itemsPerPage * (this.pageData.currentPage - 1) + 1;
                this.displayedItemLastIndex = this.displayedItemFirstIndex + this.pageData.itemsPerPage - 1;
                if (this.displayedItemLastIndex > mdata.totalRows) {
                    this.displayedItemLastIndex = mdata.totalRows;
                }
            }
        }
        this.isDataLoaded = true;
    }

    private setColumnsWidth() {
        console.log("setColumnsWidth");
        let totalWidth = 0;
        const columnsWithoutWidth: MColumn[] = [];

        this.columns.forEach(col => {
            if (col.width === null) {
                columnsWithoutWidth.push(col);
            } else {
                totalWidth += col.width;
            }
        });

        if (columnsWithoutWidth.length === 0) {
            return;
        }

        const remainedWidthPerColumn = Math.round((100 - totalWidth) / columnsWithoutWidth.length);
        columnsWithoutWidth.forEach(col => {
            col.width = remainedWidthPerColumn;
        });
    }

    private setDefaultSortDirection() {
        console.log("setDefaultSortDirection");
        let column = this.columns.find(x => x.sortable && x.sortDirection != null);
        if (column === null) {
            column = this.columns.find(x => x.sortable);
            if (column === null) {
                throw new Error("Please, set at least one column as sortable.");
            }
            column.sortDirection = MSortDirection.asc;
        }
        this.tableState.sortedColumn = column;
    }

    private setPageChangeDebounce() {
        this.pageData.currentPageControl.valueChanges
            .debounceTime(this.eventTriggerDebounceTime)
            .subscribe(selectedPage => {

                console.log("setPageChangeDebounce");

                const pnumber = Number(selectedPage);
                if (
                    isNaN(pnumber) || 
                    pnumber < 1 || 
                    pnumber > this.totalPages || 
                    this.pageData.currentPage === pnumber
                ) {
                    this.pageData.currentPageControl.setValue(this.pageData.currentPage);
                } else {
                    this.pageData.currentPage = pnumber;
                    this.loadData(true);
                }
            });
    }

    private setFilterChangeDebounce(column: MColumn) {
        column.filterValueControl.valueChanges
            .debounceTime(this.eventTriggerDebounceTime)
            .subscribe((filterValue: any) => {
                console.log("setFilterChangeDebounce");
                let isFilterChanged = false;
                switch (column.dataType) {
                    case MColumnDataType.number:
                    case MColumnDataType.currency:
                    case MColumnDataType.percent:
                        {
                            const pnumber = Number(filterValue);
                            if (!isNaN(pnumber) && column.filterValueControl.value !== column.filterValue) {
                                column.filterValue = filterValue;
                                isFilterChanged = true;
                            }

                            if (isNaN(pnumber) && column.filterValueControl.value !== column.filterValue) {
                                column.filterValueControl.setValue(column.filterValue);
                            }
                        }
                        break;
                    case MColumnDataType.string:
                    case MColumnDataType.date:
                        {
                            column.filterValue = filterValue;
                            isFilterChanged = true;
                        }
                        break;
                    default:
                        throw new Error(`Unknown column dataType (${column.dataType})`);
                }


                if (column.filterValue !== null && isFilterChanged) {
                    this.pageData.currentPage = 1;
                    this.loadData(true);
                }
            });
    }

    private onPageChange($event: Event, page: number): void {
        console.log("onPageChange");
        if ($event && $event.preventDefault) {
            $event.preventDefault();
        }
        this.isDataLoaded = false;
        this.pageData.currentPageControl.setValue(page);
    }

    private onSortDirectionChange(column: MColumn) {
        console.log("onSortDirectionChange");

        if (column.sortable) {
            if (this.tableState.sortedColumn !== column) {
                this.tableState.sortedColumn.sortDirection = null;
                this.tableState.sortedColumn = column;
            }

            column.sortDirection = column.sortDirection === MSortDirection.asc
                ? MSortDirection.desc : MSortDirection.asc;

            this.loadData(true);
        }
    }

    selectRow(row: any): void {
        if (this.moptions.onRowSelected) {
            this.moptions.onRowSelected(row);
        }
    }

    private getColumnHeaderCssClasses(column: MColumn) {
        const classes: string[] = [];
        if (this.moptions.sortable && column.sortable) {
            classes.push("sortable");
        }
        if (column.sortDirection !== null) {
            classes.push(MSortDirection[column.sortDirection]);
        }
        return classes;
    }

    private toggleFilter() {
        console.log("toggleFilter");

        this.isFilterVisible = !this.isFilterVisible;
        if (!this.isFilterVisible) {
            let filterUpdated = false;
            this.columns.forEach(col => {
                if (col.filterValue !== null) {
                    col.filterValue = null;
                    filterUpdated = true;
                }

            });
            if (filterUpdated) {
                this.pageData.currentPage = 1;
                this.loadData(true);
            }
        }
    }

    // private serverErrorToast() {
    //     this.toasterService
    //         .showError(
    //             "Server Error", 
    //             "Could not load " + this.moptions.tableType.toString().toLowerCase() + " data from the server"
    //         );
    // }
}


class MColumn implements IMColumn {
    title: string;
    name: string;
    dataType = MColumnDataType.string;
    sortable?: boolean = null;
    sortDirection?: MSortDirection = null;
    width?: number = null;
    filterable?: boolean = true;
    filterValue?: any;
    filterValueControl: FormControl;
    renderType = MColumnRenderType.default;
    suffix?: string = "";
    prefix?: string = "";
    
    get inputType() {
        switch (this.dataType) {
            case MColumnDataType.string:
            case MColumnDataType.date:
                return "text";
            case MColumnDataType.number:
            case MColumnDataType.currency:
                return "number";
            default:
                throw new Error(`Unknown column dataType (${this.dataType})`);
        }
    }


    getColumnTemplate(row: any, column: IMColumn) {
        const value = row[column.name] === null ? "" : row[column.name];
        switch (this.renderType) {
            case MColumnRenderType.default:
                switch (this.dataType) {
                    case MColumnDataType.currency:
                        return String(`${this.prefix} ${this.currencyPipe.transform(value, "USD", true, "1.0-2")} ${this.suffix}`).trim();
                    case MColumnDataType.number:
                        return String(`${this.prefix} ${this.decimalPipe.transform(value, "1.0-2")} ${this.suffix}`).trim();
                    case MColumnDataType.percent:
                        return String(`${this.prefix} ${this.decimalPipe.transform(value, "1.0-2")} ${this.suffix}`).trim();
                    case MColumnDataType.date:
                        let output = String(`${this.prefix} ${this.datePipe.transform(value, "MM/dd/yyyy")} ${this.suffix}`)
                            .trim();
                        
                        return output === "null" ? "" : output;
                    default:
                        return String(`${this.prefix} ${value} ${this.suffix}`).trim();
                }
            case MColumnRenderType.progress:
                return `
                <div class="progress">
                    <div class="progress-bar" data-progress="${value}"></div>
                </div>
                `;
            case MColumnRenderType.progressWithPercent:
                return `<div class="row">
                                <div class="col-xs-6 col-sm-6">
                                    <div class="progress">
                                        <div class="progress-bar progress-bar-success" style="width:${row.TrucksUptime}%" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-6">
                                    <span>${value}%</span>
                                </div>
                           </div>`;
            default:
                throw new Error(`Unknown column renderType (${this.renderType})`);
        }
    }


    constructor(
        column: IMColumn, 
        moptions: IADTableOptions, 
        private currencyPipe: CurrencyPipe, 
        private decimalPipe: DecimalPipe, 
        private datePipe: DatePipe
    ) {
        console.log("MColumn constructor", moptions, column);
        Object.assign(this, column);
        if (moptions.sortable && this.sortable === null) {
            this.sortable = moptions.sortable;
        }
        if (this.filterable) {
            this.filterValueControl = new FormControl(this.filterValueControl);
        }
        if (this.dataType === MColumnDataType.percent) {
            this.suffix = "%";
        }
    }
}

class MPageData implements IMPageData {
    currentPage = 1;
    itemsPerPage = 10;
    currentPageControl: FormControl;

    constructor(pageData: IMPageData) {
        Object.assign(this, pageData);
        this.currentPageControl = new FormControl(this.currentPage);
    }
}
