import { Component, ElementRef, Input, OnInit, Output, Self, ViewChild } from '@angular/core';
import { NgControl } from '@angular/forms';

import { EventEmitter } from 'eventemitter3';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements OnInit {
  @ViewChild('input', {static: true}) input: ElementRef;
  @Input() type = 'text';
  @Input() label: string;
  @Input() maxLength: number = 500;
  @Output() cardNumberEvent = new EventEmitter();

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
   }
  
  ngOnInit(): void {
    const control = this.controlDir.control;
    const validators = control.validator ? [control.validator] : [];
    const asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];

    control.setValidators(validators);
    control.setAsyncValidators(asyncValidators);
    control.updateValueAndValidity();
  }

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  onChange(event){
  }

  onTouched(){}

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  onCardChange(value: any) {
    this.cardNumberEvent.emit(value);
  }
}
