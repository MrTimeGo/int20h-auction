import { Component, inject } from '@angular/core';
import { LotService } from '../../shared/services';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateLot } from '../../shared/models';
import {
  ButtonComponent,
  FormFieldComponent,
  ImageCarouselComponent
} from '../../shared/components';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { mergeMap, of, tap } from 'rxjs';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-new-lot',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    ImageCarouselComponent,
    RouterModule,
    FormFieldComponent,
    ButtonComponent
  ],
  templateUrl: './new-lot.component.html',
  styleUrl: './new-lot.component.scss'
})
export class NewLotComponent {
  lotService = inject(LotService);
  fb = inject(FormBuilder);
  route = inject(ActivatedRoute);

  mode: 'new' | 'edit' = 'new' 

  constructor() {
    this.route.paramMap.pipe(
      tap((paramMap) => {
        this.mode = paramMap.has('id') ? 'edit' : 'new'
      }),
      mergeMap((paramMap) =>
        paramMap.has('id')
          ? this.lotService.getLotDetailed(paramMap.get('id')!)
          : of(null)
      )
    ).subscribe((lot) => {
      if (lot) {
        console.log(lot.startingAt);
        this.form.patchValue({ 
          ...lot,
          startingAt: formatDate(new Date(lot.startingAt), 'yyyy-MM-ddThh:mm', 'en-EN'),
          closingAt: formatDate(new Date(lot.closingAt), 'yyyy-MM-ddThh:mm', 'en-EN'),
          tags: lot.tags.join(' ')
        })
      }
    });
  }

  form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    startingAt: ['', Validators.required],
    closingAt: ['', Validators.required],
    initialPrice: [0, [Validators.required, Validators.min(1)]],
    minimalStep: [0, [Validators.required, Validators.min(1)]],
    tags: [''],
    images: this.fb.nonNullable.array<string>([])
  });

  get name() {
    return this.form.controls.name;
  }

  get description() {
    return this.form.controls.description;
  }

  get startingAt() {
    return this.form.controls.startingAt;
  }

  get closingAt() {
    return this.form.controls.closingAt;
  }

  get initialPrice() {
    return this.form.controls.initialPrice;
  }

  get minimalStep() {
    return this.form.controls.minimalStep;
  }

  get tags() {
    return this.form.controls.tags;
  }

  get images() {
    return this.form.controls.images;
  }

  submit() {
    this.lotService.createLot({
      ...this.form.value,
      tags: this.form.value.tags?.trim().split(' '),
      startingAt: new Date(this.form.value.startingAt as string),
        closingAt: new Date(this.form.value.closingAt as string),
    } as CreateLot);
  }
}
