import { Component, inject } from '@angular/core';
import { AuthFormComponent, ButtonComponent, FormFieldComponent } from '../../shared/components';
import { Router, RouterModule } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../shared/services';


@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [AuthFormComponent, FormFieldComponent, ButtonComponent, RouterModule, ReactiveFormsModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {
  passwordConfirmValidatorFn: ValidatorFn = (control) => {
    const origin = control.parent?.value.password;

    return origin === control.value ? null : { passwordsDontMatch: true };
  }

  authService = inject(AuthService);
  router = inject(Router);

  fb = inject(FormBuilder)
  form = this.fb.nonNullable.group({
    username: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    passwordConfirm: ['', [Validators.required, this.passwordConfirmValidatorFn]],
  });

  get username() {
    return this.form.controls.username;
  }

  get email() {
    return this.form.controls.email;
  }

  get password() {
    return this.form.controls.password;
  }

  get passwordConfim() {
    return this.form.controls.passwordConfirm;
  }

  
  step: 1 | 2 = 1;

  submit() {
    if (this.step === 1) {
      this.step = 2;
      return;
    }

    const { username, email, password } = this.form.value;

    this.authService.registerUser({
      username: username!,
      email: email!,
      password: password!
    }).subscribe();

    this.authService.getCurrentUser$().subscribe(user => {
      if (user && user.email === email) {
        this.router.navigate(['/lots']);
      }
    })
  }
}
