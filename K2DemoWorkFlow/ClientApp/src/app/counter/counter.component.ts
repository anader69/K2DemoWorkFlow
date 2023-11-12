import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent implements OnInit {

  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    
   

    if (window.location.href.indexOf("id_token") == -1) {
      this.GetUrl();
    }
    //var tenantID = "cd656731-140b-47f0-bf68-ec25880ccfff";
    //var clinetID = "a5f32666-967b-41a6-a7ff-4aa757af016e";
    //var client_secret = "RMx8Q~AJ7z7f4dlqR-oVKvIUDyfvCzD8M2FkpbWG";
    //var coedChallenge ="hyhuHUFMyDTh1qbAHPvXv0BpLh1FbWlMu2F6T0NXgeM"
  
    //var reqContent = "?response_type=code&redirect_uri=http://localhost:44415/" + "&client_id=" + clinetID + "&response_mode=query&state=12345&code_challenge_method=S256&code_challenge=" + coedChallenge + "&scope=email+openid+profile";
    //debugger;
    //if (window.location.href.indexOf("code") == -1) {
    //  window.location.href = "https://login.microsoftonline.com/" + tenantID + "/oauth2/v2.0/authorize" + reqContent;
    //}

  }

  public GetUrl() {
    this.http.get('http://localhost:5115/api/LeaveRequest/GetUrl').
      subscribe((data: any) => {
        if (data)
          window.location.href = data.url;
        console.log(data)
      })
  }
}
