import { Injectable } from "@angular/core";
import { Subject } from "rxjs/Subject";
import * as moment from "moment";

@Injectable()
export class GlobalFilter {
    startDate = moment().subtract(1, "weeks");
    endDate = moment();
    onDateRangeChanged = new Subject<any>();
}
