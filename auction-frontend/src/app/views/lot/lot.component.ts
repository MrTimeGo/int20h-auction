import { Component, inject } from '@angular/core';
import { LotService } from '../../shared/services';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FilterBoxComponent, LotCardComponent, SearchBoxComponent, SortBoxComponent } from '../../shared/components';
import { FormBuilder } from '@angular/forms';
import { LotParams, LotSortType } from '../../shared/models';
import { debounceTime, map } from 'rxjs';

@Component({
  selector: 'app-lot',
  standalone: true,
  imports: [
    LotCardComponent,
    CommonModule,
    RouterModule,
    FilterBoxComponent,
    SearchBoxComponent,
    SortBoxComponent
  ],
  templateUrl: './lot.component.html',
  styleUrl: './lot.component.scss'
})
export class LotComponent {
  private lotService = inject(LotService);
  private formBuilder = inject(FormBuilder);

  constructor() {
    this.form.valueChanges.pipe(debounceTime(500)).subscribe((value) => {
      this.lotService.applyParams(value as LotParams);
    });
  }

  form = this.formBuilder.group({
    filters: this.formBuilder.group({
      myLots: [false],
      myBets: [false],
      lotStatus: 0
    }),
    sort: this.formBuilder.group({
      type: [LotSortType.StartingAt],
      sortOrder: [undefined],
      betStepOrder: [undefined]
    }),
    searchTerm: [''],
    pagination: this.formBuilder.group({
      page: [0],
      pageSize: [12]
    })
  });

  lots$ = this.lotService.getLots$();
  pages$ = this.lotService.getCount$().pipe(
    map(c => Math.ceil(c / this.pagination.value.pageSize!)),
    map(pageCount => Array.from({ length: pageCount }).map((value, index) => index))
  );

  get filters() {
    return this.form.controls.filters;
  }

  get searchTerm() {
    return this.form.controls.searchTerm;
  }

  get sort() {
    return this.form.controls.sort;
  }

  get pagination() {
    return this.form.controls.pagination;
  }

  pageClicked(page: number) {
    this.pagination.patchValue({ page: page });
  }
}
