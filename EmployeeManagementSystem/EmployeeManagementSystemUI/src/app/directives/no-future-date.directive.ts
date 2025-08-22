import { Directive } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[noFutureDate]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: NoFutureDateDirective,
      multi: true
    }
  ]
})
export class NoFutureDateDirective implements Validator {
  validate(control: AbstractControl): ValidationErrors | null {
    if (!control.value) {
      return null; // let "required" handle empty case
    }

    const today = new Date();
    const inputDate = new Date(control.value);

    return inputDate > today ? { futureDate: true } : null;
  }
}
