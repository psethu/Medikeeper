import { Component, OnInit, Input, EventEmitter, Output, SimpleChanges} from '@angular/core';
import {Item} from '../../models/Item';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.scss']
})
export class ItemDetailComponent implements OnInit {
  @Output() addItemChild: EventEmitter<any> = new EventEmitter(); // using any because this item not formatted as the model since no id
  @Input() formData: Item;

  constructor() { }

  ngOnChanges(changes: SimpleChanges) {
    // for (const propName in changes) {
    //   console.log(propName);
    // }
    // console.log(changes.formData.currentValue);
  }

  ngOnInit(): void {
    this.resetForm();
  }  

  resetForm() {
    this.formData = {
      id: 0,
      name: '',
      cost: null
    }
  }


  onSubmit() {
    console.log("in submit");
    console.log(this.formData);
    this.addItemChild.emit(this.formData);
    this.resetForm();
  }

  
}
