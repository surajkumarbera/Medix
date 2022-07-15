import { Component, OnInit, Input } from '@angular/core';
import { ApiService } from '../../services/api/api.service';

@Component({
  selector: 'app-single-item-in-cart',
  templateUrl: './single-item-in-cart.component.html',
  styleUrls: ['./single-item-in-cart.component.css'],
})
export class SingleItemInCartComponent implements OnInit {
  @Input() cart: any;

  constructor(private _api: ApiService) {}

  ngOnInit(): void {
    //console.log(this.cart);
  }

  removeCart(cartId: any): void {
    this._api.delete('api/carts/' + cartId).subscribe(
      (result: any) => {
        alert('Item removed from cart');
      },
      (err: any) => {
        console.log(err);
      }
    );
  }
}
