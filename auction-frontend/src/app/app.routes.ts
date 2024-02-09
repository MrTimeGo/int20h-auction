import { Routes } from '@angular/router';
import { LotComponent } from './views/lot/lot.component';
import { NewLotComponent } from './views/new-lot/new-lot.component';
import { SignInComponent } from './views/sign-in/sign-in.component';

export const routes: Routes = [
  { path: '', redirectTo: '/lots', pathMatch: 'full' },
  { path: 'lots', component: LotComponent },
  { path: 'new', component: NewLotComponent },
  { path: 'sign-in', component: SignInComponent },
];
