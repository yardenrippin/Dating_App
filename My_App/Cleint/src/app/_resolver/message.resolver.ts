import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_servise/User.service';
import { AlertifyService } from '../_servise/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Message } from '../_models/Message';
import { AthuService } from '../_servise/auth.service';


@Injectable()
export class Messagesresolver implements Resolve<Message[]> {
    // tslint:disable-next-line: max-line-length
    constructor( private uerserver: UserService, private router: Router, private alertify: AlertifyService, private authservis: AthuService) {}
pageNumber = 1;
pageSize = 5;
messagecontainer = 'unread';




resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
 
    // tslint:disable-next-line: max-line-length
    return this.uerserver.getMessaged (Number(this.authservis.decoodedToken.nameid), this.pageNumber, this.pageSize, this.messagecontainer).pipe(
        catchError(error => {
            this.alertify.error('problem retriving messages');
            this.router.navigate(['/home']);
            return of(null);
        })
    )
}




}