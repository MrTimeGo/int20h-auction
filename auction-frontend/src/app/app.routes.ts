import { Routes } from '@angular/router';
import { LotComponent } from './views/lot/lot.component';
import { NewLotComponent } from './views/new-lot/new-lot.component';
import { SignInComponent } from './views/sign-in/sign-in.component';
import { SignUpComponent } from './views/sign-up/sign-up.component';
import { LotDetailsComponent } from './views/lot-details/lot-details.component';
import { authGuard } from './shared/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/lots', pathMatch: 'full' },
  { path: 'lots', component: LotComponent, canActivate: [authGuard] },
  { path: 'lots/:id', component: LotDetailsComponent, canActivate: [authGuard] },
  { path: 'edit/:id', component: NewLotComponent, canActivate: [authGuard] },
  { path: 'new', component: NewLotComponent, canActivate: [authGuard] },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
];
