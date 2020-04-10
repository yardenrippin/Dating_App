import { Component, OnInit } from '@angular/core';
import { AthuService } from '../_servise/auth.service';
import { AlertifyService } from '../_servise/alertify.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
photoutl : string;
  constructor(public authService: AthuService , private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
this.authService.currentPhotoUrl.subscribe(photoutl => this.photoutl = photoutl);
  }
  login() {
   
    this.authService.login(this.model).subscribe(next => {
      
      this.alertify.success('login successfully');
      this.router.navigate(['\members']);
      
      
    }, error => {
      this.alertify.error(error);

    }, () => {
this.router.navigate(['\members']);
    });

  }

  loggedin() {
   return this.authService.loggedin();

  }
  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decoodedToken = null;
    this.authService.currentuser = null;
    this.alertify.message (' loagged out');
    this.router.navigate(['home']);
  }
}
