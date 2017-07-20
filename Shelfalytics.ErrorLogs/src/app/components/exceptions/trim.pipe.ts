import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "trim"})
export class TrimPipe implements PipeTransform {
    transform(value: string, args: string[]): any {
        if (value !== null){
            return value.slice(0, 85) + "...";
        } else {
            return "--null--"
        }
        
    }
}