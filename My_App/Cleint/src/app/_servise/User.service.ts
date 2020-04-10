import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';




@Injectable({
  providedIn: 'root'
})
export class UserService {

baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getUsers(page?, itemPerPage?, userparams?, likesParams?): Observable<PaginatedResult<User[]>> {
const paginationResult: PaginatedResult<User []> = new PaginatedResult<User []> ();

let params = new HttpParams();

if ( page != null && itemPerPage != null) {
params = params.append('pageNumber', page);
params = params.append('pageSize', itemPerPage);
}

if (userparams != null) {
params = params.append('minAge', userparams.minAge);
params = params.append('maxAge', userparams.maxAge);
params = params.append('gender', userparams.gender);
params = params.append('orderBy', userparams.orderBy);
}

if (likesParams === 'Likers') {
  params = params.append('Likers', 'true');
}
if (likesParams === 'Likees') {
  params = params.append('Likees', 'true');
}


return this.http.get<User[]>(this.baseUrl + 'Users', {observe: 'response', params})
.pipe(
  map (response => {

    paginationResult.result = response.body;
    if (response.headers.get('pagination') != null) {
      paginationResult.pagination = JSON.parse(response.headers.get('pagination'))
    }

    return paginationResult;
  })
);
}

getuser(id): Observable<User> {
  return this.http.get<User>(this.baseUrl + 'Users/' + id);
}

updateUser(id: number, user: User) {

  return this.http.put(this.baseUrl + 'Users/' + id, user);
}
setmainphoto(userId: number, id: number) {
  return this.http.post(this.baseUrl + 'users/' + userId + '/photos/' + id + '/setMain' , {});
}
deletephoto(userId: number, id: number ) {
  return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + id );
}
sendlike(id: number, recipientId: number ){
  return this.http.post(this.baseUrl + 'users/'+ id +'/like/'+ recipientId,{});
}
}
