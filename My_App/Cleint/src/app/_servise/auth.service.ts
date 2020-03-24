import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Response } from 'selenium-webdriver/http';
import {JwtHelperService} from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';
import {BehaviorSubject} from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AthuService {
  baseurl = environment.apiUrl + 'Auth/';
 
  jwthelper = new JwtHelperService();
  decoodedToken: any;
  currentuser: User;
photoUrl = new BehaviorSubject<string>('../../assets/user.png');
currentPhotoUrl = this.photoUrl.asObservable();

  constructor(private http: HttpClient) { }

changememberphoto(photoUrl : string){
  this.photoUrl.next(photoUrl);
}
  login(model: any) {
    return this.http.post(this.baseurl + 'login', model)
      .pipe(
        map((Response: any) => {
              const user = Response;
              if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(user.user));
            this.decoodedToken = this.jwthelper.decodeToken(user.token);
            this.changememberphoto(this.currentuser.photourl);
          }
        })
      );
  }
  register(user: User) {
return this.http.post(this.baseurl + 'register' , user);

  }
loggedin() {
  const token = localStorage.getItem('token');
  return !this.jwthelper.isTokenExpired(token);

}
}
