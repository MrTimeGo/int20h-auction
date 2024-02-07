export enum LotStatus {
  NotStarted,
  Active,
  Closed
}

export interface Lot {
  name: string;
  description: string;
  startingAt: Date;
  closingAt: Date;
  initialPrice: number;
  mininalStep: number;
  status: LotStatus;
  images: string[];
}