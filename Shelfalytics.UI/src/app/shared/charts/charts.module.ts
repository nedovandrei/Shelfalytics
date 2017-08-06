import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";
import { NgaModule } from '../../theme/nga.module';
import { ProgressBarModule } from "ng2-progress-bar";
import { AppTranslationModule } from "../../app.translation.module";
import { AmChartsModule } from "@amcharts/amcharts3-angular";
import { AmChartsService } from "@amcharts/amcharts3-angular";

import { GroupPieChart } from './group-pieCharts/pieChart.component';
import { ChartistJs } from './chartistJs/chartistJs.component';
import { EasyPieChartComponent } from './easy-pie-chart/easy-pie-chart.component';
import { AmChartsComponent } from './amcharts/amcharts.component';
import { Ng2ProgressBar } from "./ng2-progress-bar/ng2-progress-bar.component";
import { AmChartsMainComponent } from './amcharts-main/amcharts-main.component';
import { AmChartsPieChartMainComponent } from './amcharts-pie-chart-main/amcharts-pie-chart-main.component';



@NgModule({
  imports: [
    CommonModule,
    NgaModule,
    FormsModule,
    ProgressBarModule,
    AppTranslationModule
  ],
  declarations: [GroupPieChart, ChartistJs, EasyPieChartComponent, AmChartsComponent, Ng2ProgressBar, AmChartsMainComponent, AmChartsPieChartMainComponent],
  exports: [GroupPieChart, ChartistJs, EasyPieChartComponent, AmChartsComponent, Ng2ProgressBar, AmChartsMainComponent, AmChartsPieChartMainComponent],
})
export class ChartsModule { }
