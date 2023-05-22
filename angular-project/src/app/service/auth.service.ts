import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiurl='http://localhost:3000/user'; //temporal route to the api
  constructor(private http:HttpClient) {
  }
  GetAll(){
    return this.http.get(this.apiurl);
  }
  getByUsername(username: string) {
    return this.http.get<any[]>(this.apiurl).pipe(
      map((response: any[]) => {
        const user = response.find((user) => user.username === username);
        return user ? user : null;
      })
    );
  }
  ProceedRegister(inputdata:any){
      return this.http.post(this.apiurl,inputdata);
  }
  isloggedin(){
    return sessionStorage.getItem('username')!=null;
  }
}
