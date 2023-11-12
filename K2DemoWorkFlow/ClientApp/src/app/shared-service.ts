import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
  userlogin = new Subject<boolean>();
  showSpinner = new Subject<boolean>();
  islogin: boolean = false;
  private apiCount = 0;
  isshowSpinner: boolean
  constructor() {
    this.isshowSpinner = false;
   this.userlogin.subscribe(data => {
     this.islogin = data
     
   })
    this.showSpinner.subscribe(data => {
      debugger;
      this.isshowSpinner = data
    })
  }



  showLoader() {
    setTimeout(() => {
      if (this.apiCount === 0) {
        this.showSpinner.next(true);
      }
      this.apiCount++;
    })
 
  }

  hideLoader() {
    setTimeout(() => {
      this.apiCount--;
      if (this.apiCount === 0) {
        this.showSpinner.next(false);
      }
    })
 
  }

}
