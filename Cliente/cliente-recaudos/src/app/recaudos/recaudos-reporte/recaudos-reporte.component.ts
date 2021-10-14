import { Component, OnInit } from '@angular/core';
import { DataReporte, IDataReporte } from 'src/app/shared/models/dataReporte';
import { RecaudoParams } from 'src/app/shared/models/recaudoParams';
import { RecaudosPorEstacion } from 'src/app/shared/models/RecaudosPorEstacion';
import { RecaudosPorFechaYEstacion } from 'src/app/shared/models/recaudosPorFechaYEstacion';
import { RecaudosService } from '../recaudos.service';

@Component({
  selector: 'app-recaudos-reporte',
  templateUrl: './recaudos-reporte.component.html',
  styleUrls: ['./recaudos-reporte.component.css']
})
export class RecaudosReporteComponent implements OnInit {

  recaudoParams : RecaudoParams;
  recaudos: DataReporte;
  recaudosFechaEstacion: RecaudosPorFechaYEstacion[] = [];
  totalCount : number;

  constructor(private recaudoService: RecaudosService) { 
    this.recaudoParams = this.recaudoService.getRecaudoParams();
  }

  ngOnInit(): void {
    this.getRecaudosReporte();
  }

  onPageChanged(event : any){
    const params = this.recaudoService.getRecaudoParams();
    if (params.pageNumber !== event) {
      params.pageNumber = event;
      this.recaudoService.setRecaudoParams(params);
      this.getRecaudosReporte();
    }
  }

  getRecaudosReporte(){
    this.recaudoService.getRecaudosReporte().subscribe(response => {
      this.recaudos = response.dataObject;
      this.recaudosFechaEstacion = response.dataObject.dataRecaudosFechaEstacion;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    });
  }
}