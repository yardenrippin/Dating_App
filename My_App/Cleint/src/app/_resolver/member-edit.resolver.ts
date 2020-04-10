import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/User';
import { UserService } from '../_servise/User.service';
import { AlertifyService } from '../_servise/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AthuService } from '../_servise/auth.service';

@Injectable()
export class MemberEditresolver implements Resolve<User> {
    // tslint:disable-next-line: max-line-length
    constructor(private authservice: AthuService, private uerserver: UserService, private router: Router, private alertify: AlertifyService){}

resolve(route: ActivatedRouteSnapshot): Observable<User>{
    return this.uerserver.getuser(this.authservice.decoodedToken.nameid).pipe(
        catchError(error=>{
            this.alertify.error('problem retriving your data');
            this.router.navigate(['/mamber']);
            return of(null);
        })
    )
}




}