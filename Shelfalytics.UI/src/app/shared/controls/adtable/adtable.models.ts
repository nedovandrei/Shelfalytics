import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";

export interface IADTableOptions {
    columns: IMColumn[];
    load: (tableState: IMTableState) => IMData | Observable<IMData>;
    tableType: MTableType;
    dataTypeName: string;
    isDataLoading?: boolean;
    pagination?: boolean;
    pageData?: IMPageData;
    sortable?: boolean;
    filterable?: boolean;
    refresh?: Subject<any>;
    onRowSelected?: (row: any) => void;
    selectPropertyName?: string;
    loadType?: MTableLoadType;
}

export interface IMColumn {
    title: string;
    name: string;
    dataType: MColumnDataType;
    sortable?: boolean;
    filterable?: boolean;
    sortDirection?: MSortDirection;
    width?: number;
    filterValue?: any;
    suffix?: string;
    prefix?: string;
    getColumnTemplate?: (row: any, column: IMColumn) => string;
    renderType?: MColumnRenderType;
}

export interface IMData {
    rows: any[];
    totalRows?: number;
}

export interface IMTableState {
    data: IMData;
    pageData?: IMPageData;
    sortedColumn?: IMColumn;
    columns: IMColumn[];
}

export interface IMPageData {
    currentPage?: number;
    itemsPerPage?: number;
}

export enum MTableLoadType {
    ajax,
    local
}

export enum MSortDirection {
    asc,
    desc
}

export enum MColumnDataType {
    string,
    number,
    currency,
    date,
    percent
}

export enum MColumnRenderType {
    default,
    progress,
    progressWithPercent
}

export class MTableType {
    static details = "Details";
    static summary = "Summary";
}

export enum MTableLoadState {
    success,
    fail,
    pending
}
