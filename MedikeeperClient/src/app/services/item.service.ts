import { Injectable } from '@angular/core';
import { HttpClient , HttpHeaders } from '@angular/common/http';
import {Item} from '../models/Item';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  route: string = 'http://localhost:5000/items';


  constructor(private http: HttpClient) { }

  public getData = () =>{
    return this.http.get(this.route);
  }

  putItem(item: Item) {
    return this.http.put(this.route + '/'+ item.id, item);
  }  
  
  // Add Item 
  addItem(item:Item):Observable<Item> {
    return this.http.post<Item>(this.route, item, httpOptions);
  }

  deleteItem(id:number) {
    return this.http.delete(this.route + '/'+ id);
  }  

}
