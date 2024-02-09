import { Component } from '@angular/core';
import { FormFieldComponent, ButtonComponent, AuthFormComponent } from '../../shared/components';


@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [FormFieldComponent, ButtonComponent, AuthFormComponent],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss'
})
export class SignInComponent {

}
