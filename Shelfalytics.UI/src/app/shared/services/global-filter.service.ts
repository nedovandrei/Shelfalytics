import { Injectable } from "@angular/core";
import { Subject } from "rxjs/Subject";
import * as moment from "moment";

@Injectable()
export class GlobalFilter {
    startDate = moment().utc(true).subtract(1, "month");
    endDate = moment().utc(true);
    onDateRangeChanged = new Subject<any>();
    showGlobalState = false;
    globalStateSubject = new Subject<boolean>();
    globalStateValue = false;
    globalStateValueSubject = new Subject<boolean>();
    clientId: number;
    userLogged = new Subject<any>();
    isAdmin: boolean;
    role: number;
    generalManagerId: number;
    supervisorId: number;
}
