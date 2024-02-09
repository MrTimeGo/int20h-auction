import { Component, inject } from '@angular/core';
import { AuthService } from '../../services';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../controls';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, ButtonComponent, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  authService = inject(AuthService);
  router = inject(Router);

  currentUser$ = this.authService.getCurrentUser$();

  signOut() {
    this.authService.signOut();
    this.router.navigate(['/sign-in']);
  }
}
