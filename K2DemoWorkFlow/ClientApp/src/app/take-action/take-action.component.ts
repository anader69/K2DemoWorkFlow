import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../shared-service';

@Component({
  selector: 'app-take-action-component',
  templateUrl: './take-action.component.html'
})
export class TakeActionComponent implements OnInit {
  ActionForm = new FormGroup({
    Comment: new FormControl(''),
    File: new FormControl(''),
  });
  selectedFile: any;
  comment: any;
  user: any;
  image: any;
  constructor(private http: HttpClient, private _router: Router, private sharedService: SharedService) { }
  ngOnInit(): void {
    this.image = this.sharedService.processInstanceitem.attachment[0];
  }


  onSelectFile(fileInput: any) {
    this.selectedFile = <File>fileInput.target.files[0];
  }

  TackAction(action: any) {
    debugger;
    //var obj = {
    //  SerialNumber: this.sharedService.processInstanceitem.serialNumber,
    //  Action: action,
    //  ProcessId: this.sharedService.processInstanceitem.processInstanceId,
    //  username: localStorage.getItem("user")

    //}
    this.comment = this.ActionForm.get("Comment")?.value;
    this.user = localStorage.getItem("user");
    const formData = new FormData();
    formData.append('SerialNumber', this.sharedService.processInstanceitem.serialNumber);
    formData.append('Action', action);
    formData.append('ProcessId', this.sharedService.processInstanceitem.processInstanceId);
    formData.append('username', this.user);
    formData.append('comment', this.comment);
    formData.append('attachment', this.selectedFile);

    this.http.post('http://localhost:5115/api/LeaveRequest/takeAction', formData)
      .subscribe(data => {
        if (data)
       
        console.log(data)
      })
  }



}
