import { IRecaudo } from "./recaudo";

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IRecaudo[];
}
  
export class Pagination implements IPagination{
  pageIndex: number;
  pageSize: number;
  count: number;
  data: IRecaudo[] = [];
}