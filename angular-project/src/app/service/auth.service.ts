import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

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
  //gets the last id from the api
  getLastId(): Observable<number> {
    return this.http.get<any[]>(this.apiurl).pipe(
      map((response: any[]) => {
        let maxId = 0;
        for (const user of response) {
          if (user.id > maxId) {
            maxId = user.id;
          }
        }
        return maxId;
      })
    );
  }
  //posts the data to the api
  ProceedRegister(inputData:any): Observable<any>{
    console.log(inputData);
    return this.http.post(this.apiurl,inputData);
  }
  isloggedin(){
    return sessionStorage.getItem('username')!=null;
  }
  
}
