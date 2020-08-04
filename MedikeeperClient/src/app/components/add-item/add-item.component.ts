import { Component, OnInit, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.scss']
})
export class AddItemComponent implements OnInit {
  @Output() addItem: EventEmitter<any> = new EventEmitter(); // using any because this item not formatted as the model since no id
  id : number; // for use as hidden field
  name: string;
  cost: number;

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit() {
    const item = {
      name: this.name,
      cost: this.cost
    }
    console.log(this.id);
    this.addItem.emit(item);
  }

  
}
