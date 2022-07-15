import { Component, OnInit } from '@angular/core';
import {  ActivatedRoute, NavigationEnd, Router } from '@angular/router';




@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
isLogin!:boolean;
name = 'Get Current Url Route Demo';
currentRoute: string | undefined;

constructor(){}

  ngOnInit(): void {

  //   if(this.router.url == "/login"){
  //     console.log(this.router.url);
  //     console.log("Entered login page");
  //     this.isLogin = true;
  //   }else{
  //     this.isLogin = false;
  //     console.log("Removed from login page");
  //   }
   }

}
