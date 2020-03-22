import { Component, OnInit } from '@angular/core';
import { AthuService } from './_servise/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import { User } from './_models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  jwthelper = new JwtHelperService();
  constructor(private authservice: AthuService) {}
  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if( token ) {
      this.authservice.decoodedToken = this.jwthelper.decodeToken(token);
    }
    if (user) {
      this.authservice.currentuser = user;
      this.authservice.changememberphoto(user.photourl);
    }
  }
}
