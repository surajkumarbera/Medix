import { AfterViewInit, Component, OnInit } from '@angular/core';
declare const $:any;

@Component({
  selector: 'app-view-orders',
  templateUrl: './view-orders.component.html',
  styleUrls: ['./view-orders.component.css']
})
export class ViewOrdersComponent implements OnInit ,AfterViewInit {

  constructor() { }
  ngAfterViewInit(): void {
    $('#mydatatable').DataTable();
}
  ngOnInit(): void {
  }

}
