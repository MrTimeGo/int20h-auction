import { Component, inject } from '@angular/core';
import { FileService, LotService } from '../../shared/services';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateLot } from '../../shared/models';
import {
  ButtonComponent,
  FormFieldComponent,
  ImageCarouselComponent
} from '../../shared/components';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { mergeMap, of, tap } from 'rxjs';
import { formatDate } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

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
  fileService = inject(FileService);
  fb = inject(FormBuilder);
  route = inject(ActivatedRoute);
  router = inject(Router);
  toastr = inject(ToastrService);

  mode: 'new' | 'edit' = 'new';
  lotId: string | null = null;

  constructor() {
    this.route.paramMap.pipe(
      tap((paramMap) => {
        this.mode = paramMap.has('id') ? 'edit' : 'new'
        this.lotId = paramMap.get('id');
      }),
      mergeMap((paramMap) =>
        paramMap.has('id')
          ? this.lotService.getLotDetailed(paramMap.get('id')!)
          : of(null)
      )
    ).subscribe((lot) => {
      if (lot) {
        this.form.patchValue({ 
          ...lot,
          startingAt: formatDate(new Date(lot.startingAt), 'yyyy-MM-ddThh:mm', 'en-EN'),
          closingAt: formatDate(new Date(lot.closingAt), 'yyyy-MM-ddThh:mm', 'en-EN'),
          tags: lot.tags.join(' ')
        })

        lot.images.forEach((i) => {
          this.images.push(new FormControl(i, { nonNullable: true }));
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
    const lot = {
      ...this.form.value,
      tags: this.form.value.tags?.trim().split(' ').filter(t => !!t),
      startingAt: new Date(this.form.value.startingAt as string),
        closingAt: new Date(this.form.value.closingAt as string),
    } as CreateLot

    if (this.mode == 'new') {
      this.lotService.createLot(lot).subscribe({
        next: (lot) => {
          this.toastr.success('Лот було створено');
          this.router.navigate(['/lots', lot.id])
        },
        error: (err) => {
          console.error(err);
          this.toastr.error('Щось пішло не так');
        }
      });
    } else {
      this.lotService.updateLot(this.lotId!, lot).subscribe({
        next: (lot) => {
          this.toastr.success('Зміни збережено');
          this.router.navigate(['/lots', lot.id])
        },
        error: (err) => {
          console.error(err);
          this.toastr.error('Щось пішло не так');
        }
      });
    }
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  onFileSelected(event: any) {
    const files: File[] = [...event.target.files];

    this.fileService.uploadFile(files).subscribe((staticFiles) => {
      staticFiles.forEach(staticFile => {
        this.form.controls.images.push(new FormControl(staticFile.url, {nonNullable: true}));
      });
    });
  }

  onImageDelete(image: string) {
    const index = this.images.controls.findIndex(c => c.value === image);
    this.images.removeAt(index);
  }
}
