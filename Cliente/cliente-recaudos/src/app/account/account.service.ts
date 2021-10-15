import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IUsuario } from '../shared/models/usuario';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  headers = new HttpHeaders({
    'Authorization' : 'Bearer ' + localStorage.getItem('token'),
    'Content-Type': 'application/json',
    'Accept': 'application/json',
  });

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<IUsuario>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string){
  
    if(token === null){
        this.currentUserSource.next(undefined);
        return of(null);
    }
     return this.http.get<IUsuario>(`${this.baseUrl}account`, {headers:this.headers}).pipe(
       map((user: IUsuario) => {
        if(user){
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
       })
     );
  }

  login(values: any){
    return this.http.post<IUsuario>(`${this.baseUrl}account/login`, values).pipe(
      map((user: IUsuario) => {
        if(user){
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(undefined);
    this.router.navigateByUrl('/');
  }
}