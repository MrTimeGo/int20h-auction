import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Message } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  http = inject(HttpClient);
  baseUrl = `${environment.baseUrl}/chat`


  constructor() { }

  sendMessage(lotId: string, text: string) {
    this.http.post(`${this.baseUrl}/lot/${lotId}`, {
      text
    }).subscribe();
  }

  getMessagesForLot(lotId: string) {
    return this.http.get<Message[]>(`${this.baseUrl}/lot/${lotId}`)
  }
}
