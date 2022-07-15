import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthorizationService } from 'src/app/services/Authorization-Service/authorization.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  showSpinner : boolean = false;

constructor(private _router:Router,private _loginservice:AuthorizationService){}

emp_loginForm = new FormGroup({
  emp_Id: new FormControl('', [
    Validators.required,
  ]),
  emp_Pwd: new FormControl('', [
    Validators.required,
    Validators.pattern(
      '^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$'
    ),
  ]),
});

empLogin() {
  console.log(this.emp_loginForm.value);
  this.showSpinner = true;
  const body = {
    email: this.emp_loginForm.value.emp_Id,
    password: this.emp_loginForm.value.emp_Pwd,
  };

  this._loginservice.post(body, 'api/signIn').subscribe(
    (result: any) => {
      localStorage.setItem('mediX_Auth', result.Authorization);
      console.log('success : ', result);
      this._router.navigate(['/dashboard']);
      //window.location.assign("http://localhost:4200/");
    },
    (error: any) => {
      console.log('error : ', error.error.Message);
      alert('Login Unsuccessfull. Please check your email & password.');
      this.showSpinner = false;
    }
  );
}

  ngOnInit(): void {
  }

}
