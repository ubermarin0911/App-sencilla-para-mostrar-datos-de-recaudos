import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: 'login',
    loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule),
  },
  { 
    path: 'recaudos', 
    loadChildren: () => import('./recaudos/recaudos.module').then(mod => mod.RecaudosModule) 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }