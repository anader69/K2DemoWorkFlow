import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  taskData:any=[]
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.getmyTask();
  }
  
  startprocess() {
    var obj = {
      username: localStorage.getItem("user"),
      AssignedTo: 'SURE\\MHANNA'
    }
    this.http.post("http://localhost:5115/api/LeaveRequest/startworkflow ",obj ).subscribe(data => {
      if (data)
        this.getmyTask();
        console.log(data)
    })

  }
  getmyTask() {

    this.http.get('http://localhost:5115/api/LeaveRequest/getusertask?Originator=' + localStorage.getItem("user"))
        .subscribe((data: any) => {
          if (data)
            this.taskData = data
          console.log(this.taskData)
        })
    }



}
