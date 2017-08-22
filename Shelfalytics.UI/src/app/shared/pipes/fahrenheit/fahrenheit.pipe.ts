import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "fahrenheit"
})
export class FahrenheitPipe implements PipeTransform {
    transform(value: any): any {
        return Math.round(value * 1.8 + 32);
    }
}