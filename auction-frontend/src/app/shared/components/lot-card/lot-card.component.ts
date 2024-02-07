import { Component, Input } from '@angular/core';
import { ImageCarouselComponent } from '../image-carousel/image-carousel.component';
import { Lot, LotStatus } from '../../models';
import { ChipComponent } from '../chip/chip.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-lot-card',
  standalone: true,
  imports: [CommonModule, ImageCarouselComponent, ChipComponent],
  templateUrl: './lot-card.component.html',
  styleUrl: './lot-card.component.scss'
})
export class LotCardComponent {
  @Input() lot: Lot = {
    name: 'Name',
    description: 'Description',
    initialPrice: 1000,
    mininalStep: 1000,
    startingAt: new Date(2024, 3, 22, 12, 0),
    closingAt: new Date(2024, 3, 22, 16, 0),
    status: LotStatus.NotStarted,
    images: [
      'https://splidejs.com/images/slides/image-slider/01.jpg',
      'https://static.nationalgeographic.co.uk/files/styles/image_3200/public/elelphants_kindness_family_1122.jpg?w=1600&h=900',
      'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSX-4zmXVcxQ5QKHrhQIsV9IUutI3IV-jF6vulRP9e4KAzJGfxgNBDKa6SPwpbEFU7y-Tc&usqp=CAU'
    ]
  }

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
    return this.statusesOptions[this.lot.status];
  }
}
