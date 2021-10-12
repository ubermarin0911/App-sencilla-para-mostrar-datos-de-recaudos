import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  token = localStorage.getItem('token');

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    req = this.addAuthenticationToken(req);

    return next.handle(req);
  }

  private addAuthenticationToken(request: HttpRequest<any>): HttpRequest<any> {
    if (!this.token) {
      return request;
    }

    return request.clone({
      headers: request.headers.set("Authorization", `Bearer ${this.token}` )
    });
  }
}
