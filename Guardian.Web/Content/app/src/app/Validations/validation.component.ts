import {Component} from '@angular/core';
import {NgForm} from '@angular/forms';
import { HttpClient } from "@angular/common/http";
 
@Component({
  selector: 'validation',
  templateUrl: './validation.component.html',
})
export class ValidationForm {

    constructor(private http: HttpClient) {

    }

  onSubmit(f: NgForm) {
    this.http.post('/guardian/api/validations', {
        Expression: f.value.expression,
        ErrorMessage: 'This is a demo of the validation system!',
        ErrorCode: 123,
        ApplicationID: 'DemoApplication'
    }).subscribe(response => {

        console.log(response);
     });
  }
}