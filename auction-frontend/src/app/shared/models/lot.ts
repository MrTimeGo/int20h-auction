import { Pagination } from ".";

export enum LotStatus {
  None = 0,
  NotStarted = 1,
  Active = 2,
  Closed = 4
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
  lotStatus: LotStatus | null;
}

export interface LotSort {
  type: LotSortType | null;
  sortOrder: LotSortOrder | null;
}

export enum LotSortType {
  StartingAt,
  ClosingAt,
}

export enum LotSortOrder {
  Ascending,
  Descending
}

export enum BetStepOrder {
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