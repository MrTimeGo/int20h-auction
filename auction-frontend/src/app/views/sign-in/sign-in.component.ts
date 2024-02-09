import { Component, inject } from '@angular/core';
import { FormFieldComponent, ButtonComponent, AuthFormComponent } from '../../shared/components';
import { Router, RouterModule } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../shared/services';


@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [FormFieldComponent, ButtonComponent, AuthFormComponent, RouterModule, ReactiveFormsModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss'
})
export class SignInComponent {
  authService = inject(AuthService);
  router = inject(Router);

  fb = inject(FormBuilder);
  form = this.fb.nonNullable.group({
    emailOrUsername: ['', Validators.required],
    password: ['', Validators.required]
  })

  get emailOrUsername() {
    return this.form.controls.emailOrUsername;
  }

  get password() {
    return this.form.controls.password;
  }

  submit() {
    const { emailOrUsername, password } = this.form.value;
    this.authService.signIn(emailOrUsername!, password!).subscribe();
    this.authService.getCurrentUser$().subscribe(user => {
      if (user && (user.email === emailOrUsername || user.username === emailOrUsername)) {
        this.router.navigate(['/lots']);
      }
    })
  }
}
