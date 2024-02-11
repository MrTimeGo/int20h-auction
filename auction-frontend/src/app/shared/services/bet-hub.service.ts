import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, filter, map } from 'rxjs';
import { Bet } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BetHubService {
  private connection: HubConnection;

  private incomingBet$ = new BehaviorSubject<{lotId: string, bet: Bet}>({lotId: '', bet: {} as Bet});

  constructor() { 
    this.connection = new HubConnectionBuilder()
      .withUrl(`${environment.baseUrl}/betHub`)
      .build();

    this.connection.on("SendBetMadeNotification", (lotId: string, bet: Bet) => {
      this.incomingBet$.next({ lotId, bet });
    })

    this.connection.start();
  }


  getIncomingBetForLotId(lotId: string) {
    return this.incomingBet$.pipe(
      filter(x => x.lotId === lotId),
      map(x => x.bet)
    )
  }
}
