import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecaudosDatosComponent } from './recaudos-datos/recaudos-datos.component';
import { RecaudosRoutingModule } from './recaudos-routing.module';
import { SharedModule } from '../shared/shared.module';
import {NgxPaginationModule} from 'ngx-pagination';
import { RecaudosReporteComponent } from './recaudos-reporte/recaudos-reporte.component';

@NgModule({
  declarations: [
    RecaudosDatosComponent,
    RecaudosReporteComponent
  ],
  imports: [
    CommonModule,
    RecaudosRoutingModule,
    SharedModule,
    NgxPaginationModule
  ]
})
export class RecaudosModule { }
