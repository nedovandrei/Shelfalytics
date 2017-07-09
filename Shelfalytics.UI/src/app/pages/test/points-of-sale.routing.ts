import { Routes, RouterModule } from '@angular/router';
import { PointsOfSaleComponent } from './points-of-sale.component';
import { PosInfoComponent } from './pos-info/pos-info.component';
import { PosListComponent } from './pos-list/pos-list.component';

const routes: Routes = [
    {
        path: '',
        component: PointsOfSaleComponent,
        children: [
            { path: '', component: PosListComponent },
            { path: 'pos-info/:id', component: PosInfoComponent }
        ]
    }
];

export const routing = RouterModule.forChild(routes);
