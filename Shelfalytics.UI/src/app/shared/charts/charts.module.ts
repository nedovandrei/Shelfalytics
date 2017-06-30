import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgaModule } from '../../theme/nga.module';
import { GroupPieChart } from "./group-pieCharts/pieChart.component";

@NgModule({
  imports: [
    CommonModule,
    NgaModule
  ],
  declarations: [GroupPieChart],
  exports: [GroupPieChart]
})
export class ChartsModule { }
