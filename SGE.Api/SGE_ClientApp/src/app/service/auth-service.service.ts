import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  private jwtHelper: JwtHelperService;
  headers!:HttpHeaders;

  constructor(private http:HttpClient) {
    this.jwtHelper = new JwtHelperService();
    var auth_token = localStorage.getItem("token");
    this.headers=new HttpHeaders({'Content-Type': 'application/json',})
    this.headers=new HttpHeaders().set( 'Authorization', `Bearer ${auth_token}`)
   }

  registerUser(data:any){
    return this.http.post<any>("https://localhost:7265/api/Auth", data, {'headers':this.headers});
  }

  loginData(data: any): Observable<any> {
    return this.http.post("https://localhost:7265/api/Auth/Login", data,{'headers':this.headers});
  }
}
