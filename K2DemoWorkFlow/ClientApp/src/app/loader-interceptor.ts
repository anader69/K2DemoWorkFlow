import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { SharedService } from './shared-service';


@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

  constructor(private service: SharedService) { }
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.service.showLoader();
    return next.handle(req).pipe(

      finalize(() =>  this.service.hideLoader() )
    );
  }
}
