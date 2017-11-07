import { Pipe, PipeTransform } from "@angular/core";
import * as moment from "moment";

@Pipe({
    name: "timespan"
})
export class TimespanPipe implements PipeTransform {
    transform(value: any): any {
        // console.log("timespan", moment(value, "dd.HH:mm:ss").format("HH : mm : ss"));

        return moment(value).format("DD.MM.YY, HH:mm:ss");
    }
}
