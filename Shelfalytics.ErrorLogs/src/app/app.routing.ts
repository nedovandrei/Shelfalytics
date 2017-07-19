import { Routes, RouterModule } from "@angular/router";
import { ModuleWithProviders } from "@angular/core";
import { ExceptionsComponent } from "./components/exceptions/exceptions.component";

export const routes: Routes = [
    {
        path: "",
        pathMatch: "full",
        redirectTo: "exceptions"
    },
    {
        path: "exceptions",
        component: ExceptionsComponent
    }
]

export const routing: ModuleWithProviders = RouterModule.forRoot(routes);