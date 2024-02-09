import { Component, inject } from '@angular/core';
import { LotService } from '../../shared/services';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LotCardComponent } from '../../shared/components';

@Component({
  selector: 'app-lot',
  standalone: true,
  imports: [LotCardComponent, CommonModule, RouterModule],
  templateUrl: './lot.component.html',
  styleUrl: './lot.component.scss'
})
export class LotComponent {
  private lotService = inject(LotService);
  
  lots$ = this.lotService.getLots$();
}
