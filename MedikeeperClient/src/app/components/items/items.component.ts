import { Component, OnInit } from '@angular/core';
import {Item} from '../../models/Item';
import { ItemService } from '../../services/item.service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent implements OnInit {
  public items : Item[]; 
  item : Item;
  submittedItem : Item = null;

  constructor(private itemService : ItemService) { }

  ngOnInit(): void {
    this.getItems();
  }

  populateForm(item:Item) {
    this.item = Object.assign({}, item); // copy the item selected into Component item object
    
  }

  getItems() {
    this.itemService.getData()
    .subscribe((result) => {
      console.log(result);
      this.items = result as Item[];
    },
    (error) => {
      console.error(error);
    });        
  }

  addItem(item:any) { 
    console.log("item component")
    console.log(item.name);
    
    // Id of zero indicates POST from form. If not zero then PUT
    if (item.id == 0) {
      this.itemService.addItem(item).subscribe(item => {
        this.items.push(item);
      });
    }
    else {
      this.itemService.putItem(item).subscribe(res => {
        this.getItems();
      }, err => {
        console.log(err);
      });      
    }
  }

  onDelete(id: number) {
    this.itemService.deleteItem(id)
      .subscribe(res => {
        this.getItems();
      },
        err => {
          console.log(err);
        })

  }

}
