import { Component } from '@angular/core';
import { AuthFormComponent, ButtonComponent, FormFieldComponent } from '../../shared/components';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [AuthFormComponent, FormFieldComponent, ButtonComponent, RouterModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {
  step: 1 | 2 = 1;
}
