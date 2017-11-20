import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  title = 'app';
  validations: any[];

  constructor(private http: HttpClient) {
      
  }

  ngOnInit(): void {
      this.http.get('/guardian/api/validations').subscribe(response => {
          this.validations = response['results'];
      });
  }
}
