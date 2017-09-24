import { Routes, RouterModule } from "@angular/router";
import { Pages } from "./pages.component";
import { ModuleWithProviders } from "@angular/core";
import { MainComponent } from "./main/main.component";
import { StatisticsComponent } from "./statistics/statistics.component";
import { SettingsComponent } from "./settings/settings.component";
import { Editors } from "./editors/editors.component";
import { AuthGuard } from "../auth/auth.guard";
// noinspection TypeScriptValidateTypes

// export function loadChildren(path) { return System.import(path); };

export const routes: Routes = [
  {
    path: "login",
    loadChildren: "app/pages/login/login.module#LoginModule"
  },
  {
    path: "register",
    loadChildren: "app/pages/register/register.module#RegisterModule",
    canActivate: [AuthGuard]
  },
  {
    path: "pages",
    component: Pages,
    children: [
      { path: "", redirectTo: "main", pathMatch: "full" },
      { path: "dashboard", loadChildren: "./dashboard/dashboard.module#DashboardModule", canActivate: [AuthGuard] },
      { path: "editors", loadChildren: "./editors/editors.module#EditorsModule", canActivate: [AuthGuard] },
      { path: "components", loadChildren: "./components/components.module#ComponentsModule", canActivate: [AuthGuard] },
      { path: "charts", loadChildren: "./charts/charts.module#ChartsModule", canActivate: [AuthGuard] },
      { path: "ui", loadChildren: "./ui/ui.module#UiModule", canActivate: [AuthGuard] },
      { path: "forms", loadChildren: "./forms/forms.module#FormsModule", canActivate: [AuthGuard] },
      { path: "tables", loadChildren: "./tables/tables.module#TablesModule", canActivate: [AuthGuard] },
      // { path: 'maps', loadChildren: './maps/maps.module#MapsModule' },
      { 
        path: "points-of-sale", 
        loadChildren: "./points-of-sale/points-of-sale.module#PointsOfSaleModule", 
        canActivate: [AuthGuard] 
      },
      { path: "main", component: MainComponent, canActivate: [AuthGuard] },
      { path: "statistics", component: StatisticsComponent, canActivate: [AuthGuard] },
      { path: "settings", component: SettingsComponent, canActivate: [AuthGuard] },
      { path: "editors", component: Editors, canActivate: [AuthGuard] }
    ]
  },

];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
