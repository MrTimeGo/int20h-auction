import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-chip',
  standalone: true,
  imports: [],
  templateUrl: './chip.component.html',
  styleUrl: './chip.component.scss'
})
export class ChipComponent {
  @Input() backgroundColor: string = 'gray-10';
  @Input() textColor: string = 'gray-70';
  @Input() label: string = '';
  @Input() isClickable = false;
  @Output() clicked = new EventEmitter<string>();

  onClick(label: string) {
    if(this.isClickable){
      this.clicked.emit(label);
    }
  }
}
