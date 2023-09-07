import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CrudServiceService {
  headers!:HttpHeaders;

  constructor(private http: HttpClient) {
    var auth_token = localStorage.getItem("token");
    this.headers=new HttpHeaders({'Content-Type': 'application/json',})
    this.headers=new HttpHeaders().set( 'Authorization', `Bearer ${auth_token}`)
  }

  createNewProduct(data: any){
    return this.http.post<any>("https://localhost:7265/api/Product/AddProduct", data, {headers: this.headers});
  }
}
