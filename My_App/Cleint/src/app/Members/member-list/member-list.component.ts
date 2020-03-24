import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/User';
import { UserService } from '../../_servise/User.service';
import { AlertifyService } from '../../_servise/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
users: User[];
user: User = JSON.parse(localStorage.getItem('user'));
genderlist = [{value: 'male', display: 'Males'}, { value: 'female', display: 'Female'}]
userparems: any = {};
pagination: Pagination;
  constructor(private route: ActivatedRoute, private userservice: UserService , private alertify: AlertifyService) { }

  ngOnInit() {


this.route.data.subscribe(data => {
this.users = data['users'].result;
this.pagination = data['users'].pagination;

});
this.userparems.gender = this.user.gender === 'female' ? 'male' : 'female';
this.userparems.minAge = 18;
this.userparems.maxAge = 99;
this.userparems.orderBy = 'lastActive';

  }
  

pageChaged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadpage();
  }

  resetFilters() {
this.userparems.gender = this.user.gender === 'female' ? 'male' : 'female';
this.userparems.minAge = 18;
this.userparems.maxAge = 99;
this.loadpage();
  }




loadpage() {
  this.userservice
  .getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userparems)
  .subscribe((res: PaginatedResult<User[]>) => {
    this.users = res.result;
    this.pagination = res.pagination;

  },
  error => {
    this.alertify.error(error);
  }
  );
}
}
