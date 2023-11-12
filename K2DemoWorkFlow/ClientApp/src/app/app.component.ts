import { Component, OnInit } from '@angular/core';
import { SharedService } from './shared-service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(public sharedService: SharedService, private http: HttpClient) { }
  ngOnInit(): void {
    debugger;
    if (localStorage.getItem('user')) {
      this.sharedService.userlogin.next(true);

    }

    if (window.location.href.indexOf("id_token") > 1) {
      this.getUserToken();
    }
  }
  title = 'app';


  getUserToken() {
    const myParam = window.location.hash.split('=')[1];

    this.http.get("http://localhost:5115/api/LeaveRequest/auth?id_token=" + myParam)
      .subscribe((data: any) => {
        if (data)
        console.log(data)
      })
  }
}
