import { Injectable } from '@angular/core';
import { BehaviorSubject, filter, map } from 'rxjs';
import { Message } from '../models/message';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private connection: HubConnection;

  private incomingMessage$ = new BehaviorSubject<{lotId: string, message: Message}>({lotId: '', message: {} as Message});

  constructor() { 
    this.connection = new HubConnectionBuilder()
      .withUrl(`${environment.baseUrl}/chatHub`)
      .build();

    this.connection.on("SendMessage", (lotId: string, message: Message) => {
      this.incomingMessage$.next({ lotId, message });
    })

    this.connection.start();
  }
  getIncomingMessagesForLot(lotId: string) {
    return this.incomingMessage$.pipe(
      filter(x => x.lotId === lotId),
      map(x => x.message)
    )
  }
}
