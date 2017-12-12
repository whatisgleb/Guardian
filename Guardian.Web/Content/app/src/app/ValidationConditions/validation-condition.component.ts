import {Component} from '@angular/core';
import {NgForm} from '@angular/forms';
import { HttpClient } from "@angular/common/http";
 
@Component({
  selector: 'validation-condition',
  templateUrl: './validation-condition.component.html',
})
export class ValidationConditionForm {

    constructor(private http: HttpClient) {

    }

  onSubmit(f: NgForm) {
    this.http.post('/guardian/api/validation-conditions', {
        Expression: f.value.expression,
        ApplicationID: 'DemoApplication'
    }).subscribe(response => {

        console.log(response);
     });
  }
}