import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/User';
import { UserService } from '../_servise/User.service';
import { AlertifyService } from '../_servise/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class Listlistresolver implements Resolve<User[]> {
    constructor(private uerserver: UserService, private router: Router, private alertify: AlertifyService) {}
pageNumber = 1;
pageSize = 5;
likesparam = 'Likers';
parems = {minAge : 18, maxAge : 99 , gender: "" };


resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
    
    return this.uerserver.getUsers(this.pageNumber, this.pageSize, this.parems, this.likesparam).pipe(
        catchError(error => {
            this.alertify.error('problem retriving data');
            this.router.navigate(['/home']);
            return of(null);
        })
    )
}




}