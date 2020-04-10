import { Component, OnInit } from '@angular/core';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { AthuService } from '../_servise/auth.service';
import { UserService } from '../_servise/User.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_servise/alertify.service';
import { User } from '../_models/User';

@Component({
  selector: 'app-Lists',
  templateUrl: './Lists.component.html',
  styleUrls: ['./Lists.component.css']
})
export class ListsComponent implements OnInit {

  pagination : Pagination;
  likeparam : string;
  users: User [];
  constructor(private authservice: AthuService,
    private userservice: UserService,
    private route: ActivatedRoute, 
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.users = data['user'].result;
      this.pagination = data ['user'].pagination;
    });
   
  }
  
loadpage() {
  this.userservice
  .getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, null, this.likeparam)
  .subscribe((res: PaginatedResult<User[]>) => {
    this.users = res.result;
    this.pagination = res.pagination;

  },
  error => {
    this.alertify.error(error);
  }
  );
}
pageChaged(event: any): void {
  this.pagination.currentPage = event.page;
  this.loadpage();
}

}
