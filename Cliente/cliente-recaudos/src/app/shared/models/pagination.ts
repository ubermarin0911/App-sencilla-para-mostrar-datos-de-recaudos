import { DataReporte, IDataReporte } from "./dataReporte";
import { IRecaudo } from "./recaudo";

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IRecaudo[];
}

export interface IPaginationReporte {
  pageIndex: number;
  pageSize: number;
  count: number;
  dataObject: IDataReporte;
}
  
export class Pagination implements IPagination{
  pageIndex: number;
  pageSize: number;
  count: number;
  data: IRecaudo[] = [];
}

export class PaginationReporte implements IPaginationReporte{
  pageIndex: number;
  pageSize: number;
  count: number;
  dataObject: DataReporte;
}