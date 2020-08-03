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

  public getItems = () => {
    let route: string = 'http://localhost:5000/items';
    this.itemService.getData(route)
    .subscribe((result) => {
      console.log(result);
      this.items = result as Item[];
    },
    (error) => {
      console.error(error);
    });
  }


  ngOnInit(): void {
  }

}
