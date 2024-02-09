import { Component, Input } from '@angular/core';
import { InputComponent } from '../input/input.component';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-form-field',
  standalone: true,
  imports: [InputComponent],
  templateUrl: './form-field.component.html',
  styleUrl: './form-field.component.scss'
})
export class FormFieldComponent {
  @Input() control = new FormControl();
  @Input() type = 'text';
  @Input() label = '';
  @Input() placeholder: string = '';
}
