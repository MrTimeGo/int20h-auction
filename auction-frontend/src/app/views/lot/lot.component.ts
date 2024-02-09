import { Component, OnInit, inject } from '@angular/core';
import { LotCardComponent } from '../../shared/components/lot-card/lot-card.component';
import { Observable } from 'rxjs';
import { Lot } from '../../shared/models';
import { LotService } from '../../shared/services';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-lot',
  standalone: true,
  imports: [LotCardComponent, CommonModule, RouterModule],
  templateUrl: './lot.component.html',
  styleUrl: './lot.component.scss'
})
export class LotComponent implements OnInit {
  lots$?: Observable<Lot[]>

  private lotService = inject(LotService);
  
  ngOnInit(): void {
    this.lots$ = this.lotService.getLots$()
  }

}
