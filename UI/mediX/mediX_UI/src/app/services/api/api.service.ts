import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  mediX_token: string;
  constructor(private _http: HttpClient) {
    this.mediX_token = localStorage.getItem('mediX_Auth') || '';
  }

  post(body: Object, rel_url: string): Observable<any> {
    const url = environment.url + rel_url;
    const headers = {
      'Content-Type': 'application/json',
      Authorization: this.mediX_token,
    };
    return this._http.post<any>(url, body, { headers });
  }

  postFile(body: Object, rel_url: string): Observable<any> {
    const url = environment.url + rel_url;
    const headers = {
      Authorization: this.mediX_token,
    };
    return this._http.post<any>(url, body, { headers });
  }

  get(rel_url: string): Observable<any> {
    const url = environment.url + rel_url;
    const headers = {
      'Content-Type': 'application/json',
      Authorization: this.mediX_token,
    };
    return this._http.get<any>(url, { headers });
  }

  delete(rel_url: string): Observable<any> {
    const url = environment.url + rel_url;
    const headers = {
      'Content-Type': 'application/json',
      Authorization: this.mediX_token,
    };
    return this._http.delete<any>(url, { headers });
  }
}
