import { Component, OnInit, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.scss']
})
export class AddItemComponent implements OnInit {
  @Output() addItem: EventEmitter<any> = new EventEmitter(); // using any because this item not formatted as the model since no id

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
  
    this.addItem.emit(item);
  }

  
}