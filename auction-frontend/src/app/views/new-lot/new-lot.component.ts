import { Component, inject } from '@angular/core';
import { LotService } from '../../shared/services';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { CreateLot } from '../../shared/models';

@Component({
  selector: 'app-new-lot',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './new-lot.component.html',
  styleUrl: './new-lot.component.scss'
})
export class NewLotComponent {
  lotService = inject(LotService);
  fb = inject(FormBuilder);

  form = this.fb.group({
    name: [''],
    description: [''],
    startingAt: [''],
    closingAt: [''],
    initialPrice: [''],
    minimalStep: [''],
  })
  submit() {
    this.lotService.createLot({ 
      ...this.form.value,
      startingAt: new Date(this.form.value.startingAt as string),
      closingAt: new Date(this.form.value.closingAt as string), 
    } as unknown as CreateLot);
  }
}
