import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-auth-form',
  standalone: true,
  imports: [],
  templateUrl: './auth-form.component.html',
  styleUrl: './auth-form.component.scss'
})
export class AuthFormComponent {
  @Input() title = '';
  @Input() subtitle = '';
  @Input() step?: number;
  @Input() errors: string[] = [];
}
