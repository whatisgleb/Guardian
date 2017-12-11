import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs/Observable';
import { HttpClient } from "@angular/common/http";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

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
  public model: any;

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

  search = (text$: Observable<string>) =>
    text$
      .debounceTime(200)
      .distinctUntilChanged()
      .map(term => term.length < 2 ? []
        : this.findNode('', term, this.objectNodes));

  findNode = (currentTerm: string, searchTerm: string, nodes: any[]): string[] => {
    
    let delimiterIdx = searchTerm.indexOf('.');

    if (delimiterIdx != -1) {

      // this is a multipart string like Document.Tags.TagID
      let nodeName = searchTerm.substr(0, delimiterIdx);
      let node = nodes.filter(n => n.NodeName == nodeName)[0];
      return this.findNode(this.formatNodeName(currentTerm, nodeName), searchTerm.substr(delimiterIdx + 1, searchTerm.length), node.ChildrenObjectGraphNodes);
    } else {
      return nodes.map(o => this.formatNodeName(currentTerm, o.NodeName)).filter(v => this.filterNodeList(v, searchTerm)).slice(0, 10);
    }
  }

  formatNodeName = (prefix: string, nodeName: string): string => {
    return !!prefix ? `${prefix}.${nodeName}` : nodeName;
  }

  filterNodeList = (value: string, searchTerm: string): boolean => {

    return !searchTerm ? true : value.toLowerCase().indexOf(searchTerm.toLowerCase()) > -1;
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
      },
      {
        "NodeName": "Document",
        "NodeType": "Guardian.Website.Models.Document, Guardian.Website, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
        "ChildrenObjectGraphNodes": [
          {
            "NodeName": "DocumentID",
            "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
          },
          {
            "NodeName": "Title",
            "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
          },
          {
            "NodeName": "Description",
            "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
          },
          {
            "NodeName": "CreatedByUser",
            "NodeType": "Guardian.Website.Models.User, Guardian.Website, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "ChildrenObjectGraphNodes": [
              {
                "NodeName": "UserID",
                "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "FirstName",
                "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "LastName",
                "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "FullName",
                "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "EMailAddress",
                "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "DateOfBirth",
                "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              }
            ]
          },
          {
            "NodeName": "ModifiedByUser",
            "NodeType": "Guardian.Website.Models.User, Guardian.Website, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            "ChildrenObjectGraphNodes": [
              {
                "NodeName": "UserID",
                "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "FirstName",
                "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "LastName",
                "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "FullName",
                "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "EMailAddress",
                "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              },
              {
                "NodeName": "DateOfBirth",
                "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
              }
            ]
          },
          {
            "NodeName": "CreatedDate",
            "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
          },
          {
            "NodeName": "ModifiedDate",
            "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
          },
          {
            "NodeName": "Tags",
            "NodeType": "System.Collections.Generic.IEnumerable`1[[Guardian.Website.Models.Tag, Guardian.Website, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            "ChildrenObjectGraphNodes": [
              {
                "NodeName": "Tag",
                "NodeType": "Guardian.Website.Models.Tag, Guardian.Website, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                "ChildrenObjectGraphNodes": [
                  {
                    "NodeName": "TagID",
                    "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                  },
                  {
                    "NodeName": "Text",
                    "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                  },
                  {
                    "NodeName": "Type",
                    "NodeType": "Guardian.Website.Models.TagType, Guardian.Website, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    "ChildrenObjectGraphNodes": [
                      {
                        "NodeName": "TagTypeID",
                        "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "Description",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "CreatedByUserID",
                        "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "ModifiedByUserID",
                        "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "CreatedDate",
                        "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "ModifiedDate",
                        "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      }
                    ]
                  },
                  {
                    "NodeName": "CreatedByUser",
                    "NodeType": "Guardian.Website.Models.User, Guardian.Website, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    "ChildrenObjectGraphNodes": [
                      {
                        "NodeName": "UserID",
                        "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "FirstName",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "LastName",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "FullName",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "EMailAddress",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "DateOfBirth",
                        "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      }
                    ]
                  },
                  {
                    "NodeName": "ModifiedByUser",
                    "NodeType": "Guardian.Website.Models.User, Guardian.Website, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    "ChildrenObjectGraphNodes": [
                      {
                        "NodeName": "UserID",
                        "NodeType": "System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "FirstName",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "LastName",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "FullName",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "EMailAddress",
                        "NodeType": "System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      },
                      {
                        "NodeName": "DateOfBirth",
                        "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                      }
                    ]
                  },
                  {
                    "NodeName": "CreatedDate",
                    "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                  },
                  {
                    "NodeName": "ModifiedDate",
                    "NodeType": "System.DateTimeOffset, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                  }
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
