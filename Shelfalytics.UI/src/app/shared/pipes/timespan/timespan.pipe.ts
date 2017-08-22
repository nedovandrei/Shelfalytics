import { Pipe, PipeTransform } from "@angular/core";
import * as moment from "moment";

@Pipe({
    name: "timespan"
})
export class TimespanPipe implements PipeTransform {
    transform(value: string): any {
        // console.log("timespan", moment(value, "dd.HH:mm:ss").format("HH : mm : ss"));

        return value.substr(0, value.length - 5);
    }
}
