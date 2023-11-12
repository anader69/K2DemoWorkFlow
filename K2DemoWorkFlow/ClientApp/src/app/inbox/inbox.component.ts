import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-inbox-component',
  templateUrl: './inbox.component.html'
})
export class InboxComponent implements OnInit {
  InboxData: any = []
  constructor(private http: HttpClient) { }
    ngOnInit(): void {
      this.getInbox()
    }
  TackAction(num: any, action: any, ProcessId:any) {
    var obj = {
      SerialNumber: num,
      Action: action,
      ProcessId: ProcessId,
      username: localStorage.getItem("user")

    }
    this.http.post('http://localhost:5115/api/LeaveRequest/takeAction',obj)
      .subscribe(data => {
        if (data)
          this.getInbox();
        console.log(data)
      })
  }


  getInbox() {
    this.http.get('http://localhost:5115/api/LeaveRequest/getInbox?username=' + localStorage.getItem("user"))
      .subscribe((data: any) => {
        if (data)
          this.InboxData = data.value
        console.log(data)
      })
  }
}
