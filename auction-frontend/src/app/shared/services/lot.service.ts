import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { CreateLot, Lot, LotParams, PaginationResult } from '../models';
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
      pageSize: 20
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

  applyParams(params: LotParams): void {
    this.params = params;

    const queryParams = []
    if (params.filters) {
      queryParams.push(...Object.entries(params.filters).map(([key, value]) => `${key}=${value}`));
    }

    if (params.sort) {
      queryParams.push(...Object.entries(params.sort).map(([key, value]) => `${key}=${value}`));
    }

    if (params.searchTerm) {
      queryParams.push(`searchTerm=${params.searchTerm}`);
    }

    if (params.pagination) {
      queryParams.push(...Object.entries(params.pagination).map(([key, value]) => `${key}=${value}`));
    }

    const queryString = queryParams.join('&');

    const url = queryString ? `${this.baseUrl}?${queryString}` : this.baseUrl;

    this.http.get<PaginationResult<Lot>>(url).subscribe(this.lots);
  }

  createLot(lot: CreateLot) {
    this.http.post<Lot>(`${this.baseUrl}`, { 
      ...lot,
    }).subscribe();
    
    this.applyParams(this.params);
  }
}
