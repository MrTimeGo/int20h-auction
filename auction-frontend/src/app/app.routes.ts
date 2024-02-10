import { Routes } from '@angular/router';
import { LotComponent } from './views/lot/lot.component';
import { NewLotComponent } from './views/new-lot/new-lot.component';
import { SignInComponent } from './views/sign-in/sign-in.component';
import { SignUpComponent } from './views/sign-up/sign-up.component';
import { LotDetailsComponent } from './views/lot-details/lot-details.component';

export const routes: Routes = [
  { path: '', redirectTo: '/sign-in', pathMatch: 'full' },
  { path: 'lots', component: LotComponent },
  { path: 'lots/:id', component: LotDetailsComponent },
  { path: 'edit/:id', component: NewLotComponent },
  { path: 'new', component: NewLotComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
];
