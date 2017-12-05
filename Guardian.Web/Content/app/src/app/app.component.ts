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
  objectNodes: any[];
  selectedNode: any;

  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {

      this.setDefaults();

      this.http.get('/guardian/api/validations').subscribe(response => {
          this.validations = response as any[];
      });

      this.http.get('/guardian/api/object-graphs').subscribe(response => {
        this.objectNodes = response as any[];
      });
  }

  setDefaults(): void {
    this.objectNodes = [
      {
        NodeName: 'Demo',
        ChildrenObjectGraphNodes: [
          {
            NodeName: 'ID',
            ChildrenObjectGraphNodes: [
            ]
          },
          {
            NodeName: 'Title',
            ChildrenObjectGraphNodes: [
            ]
          },
          {
            NodeName: 'Items',
            ChildrenObjectGraphNodes: [
              {
                NodeName: 'Item',
                ChildrenObjectGraphNodes: [
                ]
              }
            ]
          }
        ]
      }
    ]
  }

  onNodeSelected(node: any) {
    this.selectedNode = node;
  }
}
