import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Categories, SubCategories } from 'src/app/Models/product.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  mediX_token: string;
  constructor(private _http: HttpClient) {
    this.mediX_token = localStorage.getItem('mediX_Auth') || '';
  }


  //Get Categories
  getCategories():Observable<any[]>{
    const url = environment.Url + 'api/Categories';
    const headers = {
      'Content-Type': 'application/json',
      Authorization: this.mediX_token,
    };
   // console.log(this._http.get<Categories[]>(url,{headers}));
    return this._http.get<any[]>(url,{headers});
  }

  //Get SubCategories
  getSubCategories():Observable<SubCategories[]>{
    const url = environment.Url + 'api/Subcategories';
    const headers = {
      'Content-Type': 'application/json',
      Authorization: this.mediX_token,
    };
    return this._http.get<SubCategories[]>(url,{headers});
  }
}
