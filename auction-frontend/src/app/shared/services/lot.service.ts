import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { CreateLot, Lot, LotDetailed, LotParams, PaginationResult } from '../models';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LotService {
  private lots = new BehaviorSubject<PaginationResult<Lot>>({ entities: [], count: 0});
  private initialized = false;
  private baseUrl = environment.baseUrl + '/lots'
  private params: LotParams = {
    pagination: {
      page: 0,
      pageSize: 12
    },
    filters: null,
    sort: null,
    searchTerm: null
  }

  constructor(private http: HttpClient) { }

  getLots$(): Observable<Lot[]> {
    if (!this.initialized) {
      this.applyParams(this.params);
      this.initialized = true;
    }

    return this.lots.pipe(map(r => r.entities));
  }

  getCount$(): Observable<number> {
    return this.lots.pipe(map(r => r.count));
  }

  applyParams(params: LotParams): void {
    this.params = params;

    const queryParams = []
    if (params.filters) {
      queryParams.push(
        ...Object.entries(params.filters)
          .filter(([, value]) => value !== null)
          .map(([key, value]) => `${key}=${value}`)
      );
    }

    if (params.sort) {
      queryParams.push(
        ...Object.entries(params.sort)
          .filter(([, value]) => value !== null)
          .map(([key, value]) => `${key}=${value}`)
      );
    }

    if (params.searchTerm) {
      queryParams.push(`searchTerm=${encodeURIComponent(params.searchTerm)}`);
    }

    if (params.pagination) {
      queryParams.push(
        ...Object.entries(params.pagination)
          .filter(([, value]) => value !== null)
          .map(([key, value]) => `${key}=${value}`)
      );
    }

    const queryString = queryParams.join('&');

    const url = queryString ? `${this.baseUrl}?${queryString}` : this.baseUrl;

    this.http.get<PaginationResult<Lot>>(url).subscribe((lots) => {
      this.lots.next(lots);
    });
  }

  createLot(lot: CreateLot) {
    return this.http.post<Lot>(`${this.baseUrl}`, { 
      ...lot,
    }).pipe(tap(() => this.applyParams(this.params)));
  }

  updateLot(id: string, lot: CreateLot) {
    return this.http.put<Lot>(`${this.baseUrl}/${id}`, { 
      ...lot,
    }).pipe(tap(() => this.applyParams(this.params)));
  }

  getLotDetailed(id: string) {
    return this.http.get<LotDetailed>(`${this.baseUrl}/${id}`);
  }

  makeBet(lotId: string, amount: number) {
    return this.http.post(`${this.baseUrl}/${lotId}/make-bet`, { amount });
  }
}
