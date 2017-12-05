import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'object-node',
  templateUrl: './object-node.component.html',
  styleUrls: ['./object-node.component.less']
})
export class ObjectNodeComponent {
    @Input()
    node: any;
    
    @Output() 
    onNodeSelected = new EventEmitter<any>();

    selectedNode: any;

  constructor() {
      
  }

  select(node: any) {
      this.selectedNode = node;
      this.onNodeSelected.emit(node);
  }
}
