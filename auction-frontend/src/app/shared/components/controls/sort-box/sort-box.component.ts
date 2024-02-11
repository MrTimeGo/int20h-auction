import { Component, Input, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { BetStepOrder, LotSortOrder, LotSortType } from '../../../models/lot';

@Component({
  selector: 'app-sort-box',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './sort-box.component.html',
  styleUrl: './sort-box.component.scss'
})
export class SortBoxComponent {
  fb = inject(FormBuilder);
  @Input() group = this.fb.group({
    type: [LotSortType.StartingAt],
    sortOrder: [undefined],
    betStepOrder: [undefined]
  });
  isFocused = false;
  LotSortOrder = LotSortOrder;
  BetStepOrder = BetStepOrder;

  onSelect($event: Event, isLotSortOder: boolean) {
    if (isLotSortOder) {
      this.group.controls.betStepOrder.patchValue(null);
    } else {
      this.group.controls.sortOrder.patchValue(null);
    }
  }
  onClearSort() {
    this.group.controls.sortOrder.patchValue(null);
    this.group.controls.betStepOrder.patchValue(null);
  }
}
