import { Component, OnInit } from '@angular/core';
import { LoginComponent } from 'src/app/login/login.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  logout(): void {
    localStorage.removeItem('mediX_Auth');
    window.location.assign('http://localhost:4200/');
  }
}
