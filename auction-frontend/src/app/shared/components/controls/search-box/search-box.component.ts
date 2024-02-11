import { Component, Input, inject } from '@angular/core';
import { ChipComponent } from '../../chip/chip.component';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TagService } from '../../../services/tag.service';
@Component({
  selector: 'app-search-box',
  standalone: true,
  imports: [ChipComponent, ReactiveFormsModule, CommonModule],
  templateUrl: './search-box.component.html',
  styleUrl: './search-box.component.scss'
})
export class SearchBoxComponent {
  tagService = inject(TagService);
  isFocused = false;
  tags$ = this.tagService.getAllTags();
  @Input() control = new FormControl();
  onTagClicked($event: string) {
    this.control.patchValue(`${this.control.value} ${$event} `);
  }
}
