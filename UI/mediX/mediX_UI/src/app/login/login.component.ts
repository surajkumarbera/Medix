import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../services/api/api.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  showSpinner: boolean = false;
  constructor(private _router: Router, private _api: ApiService) {}
  ngOnInit(): void {}

  loginForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.pattern('[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+'),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(
        '^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$'
      ),
    ]),
  });

  login() {
    this.showSpinner = true;
    const body = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    };

    this._api.post(body, 'api/signIn').subscribe(
      (result: any) => {
        localStorage.setItem('mediX_Auth', result.Authorization);
        localStorage.setItem('mediX_UserId', result.existingUser.Id);
        console.log('success : ', result);
        window.location.assign('/');
        
      },
      (error: any) => {
        this.alertError();
        this.showSpinner = false;
      }
    );
  }
  alertError(){
    Swal.fire({
      position: 'top',
      icon: 'error',
      html: '<h5>Login Unsuccessfull.</h5><h5> Please check your email & password.</h5>',
      showConfirmButton: false,
      timer: 3000,
    })
    
  }
  get email() {
    return this.loginForm.get('email');
  }
  get password() {
    return this.loginForm.get('password');
  }
}
