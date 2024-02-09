import { Routes } from '@angular/router';
import { LotComponent } from './views/lot/lot.component';
import { NewLotComponent } from './views/new-lot/new-lot.component';
import { SignInComponent } from './views/sign-in/sign-in.component';
import { SignUpComponent } from './views/sign-up/sign-up.component';

export const routes: Routes = [
  { path: '', redirectTo: '/sign-in', pathMatch: 'full' },
  { path: 'lots', component: LotComponent },
  { path: 'new', component: NewLotComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
];
