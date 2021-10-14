import { Component, OnInit } from '@angular/core';
import { IRecaudo } from 'src/app/shared/models/recaudo';
import { RecaudoParams } from 'src/app/shared/models/recaudoParams';
import { RecaudosService } from '../recaudos.service';

@Component({
  selector: 'app-recaudos-datos',
  templateUrl: './recaudos-datos.component.html',
  styleUrls: ['./recaudos-datos.component.css']
})
export class RecaudosDatosComponent implements OnInit {

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