import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api/api.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-item-page',
  templateUrl: './item-page.component.html',
  styleUrls: ['./item-page.component.css']
})
export class ItemPageComponent implements OnInit {
  show: boolean = false;
  isAuthorized! : boolean;
  id!: number;
  name!: string;
  vendor: string = 'vendor name';
  category: string = 'category name';
  price!: number;
  description: string = 'product desc';
  img_url: string = '';
  showSpinner : boolean = false;
  constructor(private router: Router, private _api: ApiService) {
    this.name = this.router.getCurrentNavigation()?.extras.state!['name'];
    this.price = this.router.getCurrentNavigation()?.extras.state!['price'];
    this.img_url = this.router.getCurrentNavigation()?.extras.state!['img'];
    this.category = this.router.getCurrentNavigation()?.extras.state!['cat'];
    this.id = this.router.getCurrentNavigation()?.extras.state!['id'];
    this.description = this.router.getCurrentNavigation()?.extras.state!['desc'];
    this.vendor = this.router.getCurrentNavigation()?.extras.state!['vendor'];
  }
  ngOnInit(): void {
    this._api.get('api/authorized').subscribe(
      (result: any) => {
        if (result) {
          this.isAuthorized = true;
        }
      },
      (error: any) => {
        this.isAuthorized = false;
      }
    );
  }

  showfun() {
    this.show = true;
  }
  hidefun() {
    this.show = false;
  }
  addToCart(productId: any): void {
    this.showSpinner = true;
    this._api
      .post(
        {
          UserId: localStorage.getItem('mediX_UserId'),
          ProductsId: productId,
          Quantity: 1,
        },
        'api/carts'
      )
      .subscribe(
        (result: any) => {
          this.showSuccess();
          this.showSpinner = false;
        },
        (err: any) => {
          this.showFailure();
          this.showSpinner = false;
        }
      );
  }
  showSuccess(){
    Swal.fire({
      position: 'top',
      icon: 'success',
      title: 'Successfully Added to Cart!',
      showConfirmButton: false,
      timer: 2000
    })
  }
  showFailure(){
    Swal.fire({
      position: 'top',
      icon: 'error',
      title: 'An Error Occurred!',
      showConfirmButton: false,
      timer: 2000
    })
  }
}

