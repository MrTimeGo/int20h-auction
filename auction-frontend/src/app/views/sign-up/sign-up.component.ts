import { Component } from '@angular/core';
import { AuthFormComponent, ButtonComponent, FormFieldComponent } from '../../shared/components';


@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [AuthFormComponent, FormFieldComponent, ButtonComponent],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {
  step: 1 | 2 = 1;
}
