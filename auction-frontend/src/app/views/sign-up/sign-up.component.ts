import { Component, inject } from '@angular/core';
import { AuthFormComponent, ButtonComponent, FormFieldComponent } from '../../shared/components';
import { Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
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
    const password = control.value.password;
    const passwordConfim = control.value.passwordConfirm;

    if (password !== passwordConfim) {
      (control as FormGroup).controls['passwordConfirm'].setErrors({ passwordsNotMatch: true });
    } else {
      (control as FormGroup).controls['passwordConfirm'].setErrors(null);
    }

    return password !== passwordConfim ? { passwordsNotMatch: true } : null;
  }

  authService = inject(AuthService);
  router = inject(Router);

  fb = inject(FormBuilder)
  form = this.fb.nonNullable.group({
    username: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    passwordConfirm: ['', Validators.required],
  }, {validators: this.passwordConfirmValidatorFn});

  get username() {
    return this.form.controls.username;
  }

  get usernameErrors() {
    const errors: string[] = [];

    if (this.username.errors?.['required']) {
      errors.push("Поле є обов'язковим")
    }

    return errors
  }

  get email() {
    return this.form.controls.email;
  }

  get emailErrors() {
    const errors: string[] = [];

    if (this.email.errors?.['required']) {
      errors.push("Поле є обов'язковим")
    }

    if (this.email.errors?.['email']) {
      errors.push('Email неправильного формату')
    }
    return errors;
  }

  get password() {
    return this.form.controls.password;
  }

  get passwordErorrs() {
    const errors: string[] = [];

    if (this.password.errors?.['required']) {
      errors.push("Поле є обов'язковим")
    }

    if (this.password.errors?.['minlength']) {
      errors.push('Довжина паролю має бути не менше 6 символів')
    }
    return errors;
  }

  get passwordConfirm() {
    return this.form.controls.passwordConfirm;
  }

  get passwordConfirmErrors() {
    const errors: string[] = [];

    if (this.passwordConfirm.errors?.['required']) {
      errors.push("Поле є обов'язковим")
    }

    if (this.form.errors?.['passwordsNotMatch']) {
      errors.push('Паролі не збігаються')
    }
    return errors;
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
