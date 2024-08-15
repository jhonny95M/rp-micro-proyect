export const PAGE = 1;
export const SIZE = 10;
export const ORDER = "descend";

export interface IPaginationRequest {
  searchFilter: string;
  pageCurrent: number;
  pageSize: number;
}

export interface IPaginationResponse<T> {
  data?: T[];
  paging: {
    currentIndex: number;
    pageSize: number;
    totalResults: number;
  };
  sorting?: string[];
}

export interface IPaginationTable<T> {
  page: number;
  size: number;
  total: number;
  data?: T[];
  loading: boolean;
}

export const FILTER: IPaginationRequest = {
  pageSize: SIZE,
  pageCurrent: PAGE,
  searchFilter: "",
};
