import { Component, inject } from '@angular/core';
import { ImageCarouselComponent } from "../../shared/components/image-carousel/image-carousel.component";
import { ChipComponent } from '../../shared/components';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from "../../shared/components/controls/button/button.component";
import { FormFieldComponent } from "../../shared/components/controls/form-field/form-field.component";
import { BetHubService, LotService } from '../../shared/services';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { catchError, map, of, switchMap, tap } from 'rxjs';
import { CountdownComponent } from 'ngx-countdown';
import { LotDetailed, LotStatus } from '../../shared/models';
import { ToastrService } from 'ngx-toastr';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-lot-details',
  standalone: true,
  templateUrl: './lot-details.component.html',
  styleUrl: './lot-details.component.scss',
  imports: [ImageCarouselComponent, ChipComponent, CommonModule, ButtonComponent, FormFieldComponent, CountdownComponent, RouterModule]
})
export class LotDetailsComponent {
  lotService = inject(LotService);
  router = inject(Router);
  route = inject(ActivatedRoute);
  toastr = inject(ToastrService);
  betHub = inject(BetHubService);

  lotStatus = LotStatus;

  constructor() {
    this.route.paramMap.pipe(
      switchMap(paramMap => this.lotService.getLotDetailed(paramMap.get('id')!)),
      map(lot => ({...lot, bets: lot.bets.sort((a, b) => b.amount - a.amount)})),
      tap((lot) => {
        if (lot.status === LotStatus.NotStarted) {
          this.remainedTime = (new Date(lot.startingAt).getTime() - Date.now()) / 1000;
        } else if (lot.status === LotStatus.Active) {
          this.remainedTime = (new Date(lot.closingAt).getTime() - Date.now()) / 1000;
        }
  
        const minimumBet = (lot.bets.length ? lot.bets[0].amount : lot.initialPrice) + lot.minimalStep
  
        this.betControl = new FormControl(null, {validators: [Validators.required, Validators.min(minimumBet)] })
  
        const members = [...new Set( lot.bets.map(b => b.author))];
  
  
        // TODO: review
        members.reduce((colors, member, i) => {
          colors[member] = this.colorPalette[(i + this.colorPalette.length) % this.colorPalette.length];
          return colors;
        }, this.memberColors);
      }),
      catchError((err) => { 
        console.error(err);
        this.router.navigate(['/lots']); return of(null)
      })
    ).subscribe(lot => {
      this.lot = lot;
      this.betHub.getIncomingBetForLotId(lot!.id).subscribe((bet) => {
        this.lot!.bets = [...this.lot!.bets, bet].sort((a, b) => b.amount - a.amount)
      })
    });

  }

  lot: LotDetailed | null = null; 

  get members() {
    return Object.keys(this.memberColors);
  }

  betControl?: FormControl;

  get betErrors() {
    const errors: string[] = [];

    if (this.betControl?.hasError('min')) {
      errors.push('Ставка замала');
    }

    return errors;
  }

  remainedTime: number = 0;

  memberColors: Record<string, string> = {};
  colorPalette = ['primary-100', 'yellow', 'green', 'red'];

  makeBet(lotId: string, amount: number) {
    this.lotService.makeBet(lotId, amount).subscribe({
      next: () => {
        this.toastr.success('Ставка прийнята')
      },
      error: (err) => {
        this.toastr.error('Щось пішло не так');
        console.error(err);
      }
    });
  }
}
