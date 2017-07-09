import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgaModule } from '../../theme/nga.module';
import { GroupPieChart } from './group-pieCharts/pieChart.component';
import { ChartistJs } from './chartistJs/chartistJs.component';
import { EasyPieChartComponent } from './easy-pie-chart/easy-pie-chart.component';
import { AmChartsComponent } from './amcharts/amcharts.component';

@NgModule({
  imports: [
    CommonModule,
    NgaModule
  ],
  declarations: [GroupPieChart, ChartistJs, EasyPieChartComponent, AmChartsComponent],
  exports: [GroupPieChart, ChartistJs, EasyPieChartComponent, AmChartsComponent],
})
export class ChartsModule { }
