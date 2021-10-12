import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable} from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IPagination, Pagination } from '../shared/models/pagination';
import { RecaudoParams } from '../shared/models/recaudoParams';

@Injectable({
  providedIn: 'root'
})
export class RecaudosService {
  baseUrl = environment.apiUrl;
  pagination = new Pagination();
  recaudoParams = new RecaudoParams();

  constructor(private http: HttpClient) { }

  getRecaudos(){
    let params = new HttpParams();

    params = params.append('ordenar', this.recaudoParams.ordenar);
    params = params.append('pageIndex', this.recaudoParams.pageNumber.toString());
    params = params.append('pageSize', this.recaudoParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'recaudos', {observe: 'response', params})
      .pipe(
        map(response => {
          this.pagination = response.body;
          return this.pagination;
        })
      );
  }


  getRecaudosReporte(){
    let params = new HttpParams();

    params = params.append('ordenar', this.recaudoParams.ordenar);
    params = params.append('pageIndex', this.recaudoParams.pageNumber.toString());
    params = params.append('pageSize', this.recaudoParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'recaudos/dataReporteRecaudo', {observe: 'response', params})
      .pipe(
        map(response => {
          this.pagination = response.body;
          return this.pagination;
        })
      );
  }


  setRecaudoParams(params: RecaudoParams){
    this.recaudoParams = params;
  }

  getRecaudoParams(){
    return this.recaudoParams;
  }

}