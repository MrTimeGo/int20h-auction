import { Component, Input, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { LotStatus } from '../../../models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-filter-box',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './filter-box.component.html',
  styleUrl: './filter-box.component.scss'
})
export class FilterBoxComponent {
  fb = inject(FormBuilder);
  @Input() group: FormGroup = this.fb.group({
    myLots: [false],
    myBets: [false],
    lotStatus: 0
  });
  isFocused = false;
  LotStatus = LotStatus;
  ordersData = [LotStatus.Active, LotStatus.Closed, LotStatus.NotStarted];

  get lotStatus() {
    return this.group.controls['lotStatus'];
  }
  get activeStatus() {
    return (this.lotStatus.value & LotStatus.Active) === LotStatus.Active;
  }
  get notStartedStatus() {
    return (this.lotStatus.value & LotStatus.NotStarted) === LotStatus.NotStarted;
  }
  get closedStatus() {
    return (this.lotStatus.value & LotStatus.Closed) === LotStatus.Closed;
  }
  onCheckChange($event: Event, status: LotStatus) {
    this.group.markAsDirty();
    console.log($event);
    if ((this.lotStatus.value & status) !== status) {
      this.lotStatus.patchValue(this.lotStatus.value | status);
    }
    else {
      this.lotStatus.patchValue(this.lotStatus.value - status);
    }
  }
}
