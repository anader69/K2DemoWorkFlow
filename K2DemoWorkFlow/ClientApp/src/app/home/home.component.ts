import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  InboxData:any=[]
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.getInbox();
  }
  
  startprocess() {
    this.http.get("http://localhost:5115/api/LeaveRequest").subscribe(data => {
      if (data)
        console.log(data)
    })

  }


  TackAction(num:any,action:any) {
    this.http.get('http://localhost:5115/api/LeaveRequest/takeAction?SerialNumber=' + num + '&Action=' + action)
      .subscribe(data => {
        if (data)
          this.getInbox();
        console.log(data)
    })
  }


  getInbox() {
    this.http.get('http://localhost:5115/api/LeaveRequest/getInbox')
      .subscribe((data:any) => {
        if (data)
          this.InboxData = data.value
          console.log(data)
      })
  }
}
