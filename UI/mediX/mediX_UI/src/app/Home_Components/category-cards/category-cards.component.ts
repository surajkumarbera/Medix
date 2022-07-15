import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api/api.service';

@Component({
  selector: 'app-category-cards',
  templateUrl: './category-cards.component.html',
  styleUrls: ['./category-cards.component.css'],
})
export class CategoryCardsComponent implements OnInit {
  categoryList: any;
  subCategoryList: any;

  constructor(private _api: ApiService) {
    this.categoryList = null;
    this.subCategoryList = null;
  }

  ngOnInit(): void {
    this._api.get('api/Categories').subscribe(
      (result: any) => {
        this.categoryList = result;
        //console.log(this.categoryList);
      },
      (err: any) => {
        console.log(err);
      }
    );

    this._api.get('api/Subcategories').subscribe(
      (result: any) => {
        this.subCategoryList = result;
        //console.log(this.subCategoryList);
      },
      (err: any) => {
        console.log(err);
      }
    );
  }
}
