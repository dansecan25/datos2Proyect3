import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiurl='http://localhost:3000/user';
  constructor(private http:HttpClient) {
   }
   GetAll(){
    return this.http.get(this.apiurl);
   }
   GetByCode(code:any){
    return this.http.get(this.apiurl+'/'+code);
   }
   ProceedRegister(inputdata:any){
      return this.http.post(this.apiurl,inputdata);
   }
}
