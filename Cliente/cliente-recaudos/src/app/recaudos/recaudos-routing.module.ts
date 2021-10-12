import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecaudosDatosComponent } from './recaudos-datos/recaudos-datos.component';
import { RecaudosReporteComponent } from './recaudos-reporte/recaudos-reporte.component';

const routes: Routes = [
  {path: '', component: RecaudosDatosComponent},
  {path: 'reporte', component: RecaudosReporteComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})

export class RecaudosRoutingModule { }