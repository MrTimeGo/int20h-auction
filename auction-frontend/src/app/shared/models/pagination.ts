export interface Pagination {
  page: number;
  pageSize: number;
}

export interface PaginationResult<T> {
  entities: T[];
  count: number;
}