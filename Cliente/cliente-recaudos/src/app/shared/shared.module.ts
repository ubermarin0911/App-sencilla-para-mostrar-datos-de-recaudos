import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    TextInputComponent,
    NavBarComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    PaginationModule,
    RouterModule
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    TextInputComponent,
    PaginationModule,
    NavBarComponent,
    RouterModule
  ]
})
export class SharedModule { }