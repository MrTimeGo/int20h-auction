import { Component } from '@angular/core';
import { LotCardComponent } from '../../shared/components/lot-card/lot-card.component';

@Component({
  selector: 'app-lot',
  standalone: true,
  imports: [LotCardComponent],
  templateUrl: './lot.component.html',
  styleUrl: './lot.component.scss'
})
export class LotComponent {

}
