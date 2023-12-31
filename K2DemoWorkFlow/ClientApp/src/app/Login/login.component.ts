import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../shared-service';

@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
 loginForm = new FormGroup({
   username: new FormControl(),
   password: new FormControl()
 })
  constructor(private http: HttpClient, private _router: Router, private sharedService: SharedService) { }

  ngOnInit(): void {
      
    }



  onSubmit() {
    var obj = {
      "UserName": this.loginForm.value.username,
      "Password": this.loginForm.value.password,
    }
    this.http.post('http://localhost:5115/api/User', obj)
      .subscribe((data:any) => {
        if (data.login) {
          this.sharedService.userlogin.next(true);
          localStorage.setItem('user', data.user);
          this._router.navigate(['/home'])
        }
        console.log(data)
      })

  }
}
