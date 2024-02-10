import { Component, inject } from '@angular/core';
import { ImageCarouselComponent } from "../../shared/components/image-carousel/image-carousel.component";
import { ChipComponent } from '../../shared/components';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from "../../shared/components/controls/button/button.component";
import { FormFieldComponent } from "../../shared/components/controls/form-field/form-field.component";
import { LotService } from '../../shared/services';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, of, switchMap, tap } from 'rxjs';
import { CountdownComponent } from 'ngx-countdown';
import { LotStatus } from '../../shared/models';

@Component({
  selector: 'app-lot-details',
  standalone: true,
  templateUrl: './lot-details.component.html',
  styleUrl: './lot-details.component.scss',
  imports: [ImageCarouselComponent, ChipComponent, CommonModule, ButtonComponent, FormFieldComponent, CountdownComponent]
})
export class LotDetailsComponent {
  lotService = inject(LotService);
  router = inject(Router);
  route = inject(ActivatedRoute);

  lotStatus = LotStatus;

  lot$ = this.route.paramMap.pipe(
    switchMap(paramMap => this.lotService.getLotDetailed(paramMap.get('id')!)),
    tap((lot) => {
      if (lot.status === LotStatus.NotStarted) {
        this.remainedTime = (new Date(lot.startingAt).getTime() - Date.now()) / 1000;
      } else if (lot.status === LotStatus.Active) {
        this.remainedTime = (new Date(lot.closingAt).getTime() - Date.now()) / 1000;
      }

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
  );

  remainedTime: number = 0;

  memberColors: Record<string, string> = {};
  colorPalette = ['primary-100', 'yellow', 'green', 'red'];
}
