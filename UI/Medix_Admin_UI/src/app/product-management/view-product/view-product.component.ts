import { AfterViewInit, Component, OnInit } from '@angular/core';
declare const $:any;
@Component({
  selector: 'app-view-product',
  templateUrl: './view-product.component.html',
  styleUrls: ['./view-product.component.css']
})
export class ViewProductComponent implements OnInit, AfterViewInit{

  constructor() { }



  ngAfterViewInit(): void {
      $('#mydatatable').DataTable();
    
  
  }
  ngOnInit(): void {
   
  }

}
