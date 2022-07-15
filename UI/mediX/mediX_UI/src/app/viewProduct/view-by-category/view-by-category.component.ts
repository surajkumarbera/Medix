import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api/api.service';

@Component({
  selector: 'app-view-by-category',
  templateUrl: './view-by-category.component.html',
  styleUrls: ['./view-by-category.component.css'],
})
export class ViewByCategoryComponent implements OnInit {
  categoryType: string;
  categoryValue: string;
  productList: any;
  constructor(private _route: ActivatedRoute, private _api: ApiService) {
    this.categoryType = '';
    this.categoryValue = '';
    this.productList = [];
  }

  ngOnInit(): void {
    this._route.params.subscribe((params: any) => {
      this.categoryType = params['categoryType'];
      this.categoryValue = params['categoryValue'];

      //console.log(this.categoryType, this.categoryValue);
      let url = 'api/products/' + this.categoryType + '/' + this.categoryValue;
      this._api.get(url).subscribe(
        (result: any) => {
          //console.log(result);
          this.productList = result;
        },
        (err: any) => {
          console.log(err);
        }
      );
    });
  }
}
