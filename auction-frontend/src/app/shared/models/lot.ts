import { Pagination } from ".";

export enum LotStatus {
  NotStarted,
  Active,
  Closed
}

export interface Lot {
  id: string;
  name: string;
  description: string;
  startingAt: Date;
  closingAt: Date;
  initialPrice: number;
  minimalStep: number;
  status: LotStatus;
  images: string[];
  tags: string[];
}

export interface LotFilter {
  myLots: boolean
  myBets: boolean;
  lotStatus: LotStatus;
}

export interface LotSort {
  type: LotSortType;
  sortOrder: LotSortOrder;
}

export enum LotSortType {
  StartingAt,
  ClosingAt,
}

export enum LotSortOrder {
  Ascending,
  Descending
}

export interface LotParams {
  filters: LotFilter | null,
  sort: LotSort | null,
  searchTerm: string | null,
  pagination: Pagination | null
}

export type CreateLot = Omit<Lot, 'id' | 'status'>