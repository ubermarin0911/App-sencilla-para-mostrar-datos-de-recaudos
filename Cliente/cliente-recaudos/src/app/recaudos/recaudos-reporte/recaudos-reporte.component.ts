import { Component, OnInit } from '@angular/core';
import { IRecaudo } from 'src/app/shared/models/recaudo';
import { RecaudoParams } from 'src/app/shared/models/recaudoParams';
import { RecaudosService } from '../recaudos.service';

@Component({
  selector: 'app-recaudos-reporte',
  templateUrl: './recaudos-reporte.component.html',
  styleUrls: ['./recaudos-reporte.component.css']
})
export class RecaudosReporteComponent implements OnInit {

  recaudoParams : RecaudoParams;
  recaudos: IRecaudo[] = [];
  totalCount : number;

  constructor(private recaudoService: RecaudosService) { 
    this.recaudoParams = this.recaudoService.getRecaudoParams();
  }

  ngOnInit(): void {
    this.getRecaudos();
  }

  onPageChanged(event : any){
    const params = this.recaudoService.getRecaudoParams();
    if (params.pageNumber !== event) {
      params.pageNumber = event;
      this.recaudoService.setRecaudoParams(params);
      this.getRecaudos();
    }
  }

  getRecaudos(){
    this.recaudoService.getRecaudos().subscribe(response => {
      this.recaudos = response.data;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    });
  }

}