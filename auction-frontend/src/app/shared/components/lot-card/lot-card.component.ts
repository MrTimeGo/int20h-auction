import { Component, Input } from '@angular/core';
import { ImageCarouselComponent } from '../image-carousel/image-carousel.component';
import { Lot, LotStatus } from '../../models';
import { ChipComponent } from '../chip/chip.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-lot-card',
  standalone: true,
  imports: [CommonModule, ImageCarouselComponent, ChipComponent, RouterModule],
  templateUrl: './lot-card.component.html',
  styleUrl: './lot-card.component.scss'
})
export class LotCardComponent {  
  @Input() lot?: Lot;
  
  lotStatusEnum = LotStatus;
  
  private statusesOptions = {
    [LotStatus.NotStarted]: {
      bgColor: 'gray-10',
      textColor: 'gray-70',
      label: 'Не почався'
    },
    [LotStatus.Active]: {
      bgColor: 'green',
      textColor: 'white',
      label: 'Почався'
    },
    [LotStatus.Closed]: {
      bgColor: 'red',
      textColor: 'white',
      label: 'Закінчився'
    }
  }

  get status() {
    return this.statusesOptions[this.lot!.status as 1 | 2 | 4];
  }
}
