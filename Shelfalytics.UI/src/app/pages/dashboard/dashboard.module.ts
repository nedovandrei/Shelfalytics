import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { NgaModule } from "../../theme/nga.module";

import { Dashboard } from "./dashboard.component";
import { routing } from "./dashboard.routing";

import { PopularApp } from "./popularApp";
import { PieChart } from "./pieChart";
import { TrafficChart } from "./trafficChart";
import { UsersMap } from "./usersMap";
import { LineChart } from "./lineChart";
import { Feed } from "./feed";
import { Todo } from "./todo";
import { Calendar } from "./calendar";
import { CalendarService } from "./calendar/calendar.service";
import { FeedService } from "./feed/feed.service";
import { LineChartService } from "./lineChart/lineChart.service";
import { PieChartService } from "./pieChart/pieChart.service";
import { TodoService } from "./todo/todo.service";
import { TrafficChartService } from "./trafficChart/trafficChart.service";
import { UsersMapService } from "./usersMap/usersMap.service";
import { ControlsModule } from "../../shared/controls/controls.module";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule.forChild(),
    NgaModule,
    routing,
    ControlsModule
  ],
  declarations: [
    PopularApp,
    PieChart,
    TrafficChart,
    UsersMap,
    LineChart,
    Feed,
    Todo,
    Calendar,
    Dashboard
  ],
  providers: [
    CalendarService,
    FeedService,
    LineChartService,
    PieChartService,
    TodoService,
    TrafficChartService,
    UsersMapService
  ]
})
export class DashboardModule {}
