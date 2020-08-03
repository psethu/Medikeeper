import { Component, OnInit } from '@angular/core';
import {Item} from '../../models/Item';
import { ItemService } from '../../services/item.service'

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent implements OnInit {
  public items : Item[];  

  constructor(private itemService : ItemService) { }

  ngOnInit(): void {
    this.itemService.getData()
    .subscribe((result) => {
      console.log(result);
      this.items = result as Item[];
    },
    (error) => {
      console.error(error);
    });    
  }

  addItem(item:Item) {
    this.itemService.addItem(item).subscribe(item => {
      this.items.push(item);
    });
  }  

}
