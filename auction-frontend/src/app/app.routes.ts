import { Routes } from '@angular/router';
import { LotComponent } from './views/lot/lot.component';

export const routes: Routes = [
  { path: '', redirectTo: '/lots', pathMatch: 'full' },
  { path: 'lots', component: LotComponent },
];
