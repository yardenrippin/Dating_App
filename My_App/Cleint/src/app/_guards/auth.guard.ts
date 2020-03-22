import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import {AthuService} from '../_servise/auth.service';
import { AlertifyService } from '../_servise/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

constructor(
  private authService: AthuService,
  private router: Router,
  private alertify: AlertifyService
) {}
  canActivate(): boolean  {
    if(this.authService.loggedin()) {
      return true;
    }
    this.alertify.error('you shall not pass!!!');
    this.router.navigate(['/home']);
    return false;
  }
  
}
