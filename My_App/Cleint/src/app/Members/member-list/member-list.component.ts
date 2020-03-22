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
pagination: Pagination;
  constructor(private route: ActivatedRoute, private userservice: UserService , private alertify: AlertifyService) { }

  ngOnInit() {
this.route.data.subscribe(data => {
this.users = data['users'].result;
this.pagination = data['users'].pagination;
});
  }
  pageChaged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadpage();
  }
loadpage() {
  this.userservice.getUsers(this.pagination.currentPage,this.pagination.itemsPerPage)
  .subscribe((res: PaginatedResult<User[]>) => {
    this.users = res.result;
    this.pagination = res.pagination;
  }, error => { this.alertify.error(error); } 
  );
}
}
