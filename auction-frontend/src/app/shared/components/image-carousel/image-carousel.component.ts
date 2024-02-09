import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-image-carousel',
  standalone: true,
  imports: [],
  templateUrl: './image-carousel.component.html',
  styleUrl: './image-carousel.component.scss'
})
export class ImageCarouselComponent {
  currentImageIndex: number = 0;

  @Input() images: string[] = []

  next() {
    this.currentImageIndex = (this.currentImageIndex + 1) % this.images.length
  }

  previous() {
    this.currentImageIndex = (this.currentImageIndex - 1 + this.images.length) % this.images.length
  }
}
