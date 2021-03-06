import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/User';
import { UserService } from '../_servise/User.service';
import { AlertifyService } from '../_servise/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MemberDetailresolver implements Resolve<User> {
    constructor(private uerserver: UserService, private router: Router, private alertify: AlertifyService) {}

resolve(route: ActivatedRouteSnapshot): Observable<User>{
    return this.uerserver.getuser(route.params['id']).pipe(
        catchError(error =>{
            this.alertify.error('problem retriving data');
            this.router.navigate(['/mamber']);
            return of(null);
        })
    );
}




}