import { Component, OnInit } from '@angular/core';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { Message } from '../_models/Message';
import { UserService } from '../_servise/User.service';
import { AthuService } from '../_servise/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_servise/alertify.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[];
  pagination: Pagination;
  messageContainer: 'Unread';
  
  constructor(private userservie: UserService, private atuhservic: AthuService, private route: ActivatedRoute,
     private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this. messages = data['messages'].result;
      this.pagination = data['messages'].pagination;
    });

  }
  loadMessage() {
    
    this.userservie.getMessaged(this.atuhservic.decoodedToken.nameid, this.pagination.currentPage,
      this.pagination.itemsPerPage,
      this.messageContainer).subscribe((res: PaginatedResult<Message[]>)=>{
this.messages = res.result;

this.pagination = res.pagination;


      } , error =>{this.alertify.error(error);});
  }

  deletemessage(id: number) {
 
this.alertify.confirm('are you sure you want to delete this Meassge ?', () => {
  this.userservie.deletemassage( id, Number(this.atuhservic.decoodedToken.nameid)).subscribe(() => {
    this.messages.splice(this.messages.findIndex(M => M.id === id) , 1 );
    this.alertify.success('Message has been deleted');
  }, error => this.alertify.error('faild to delete the message'));
});
  }

pageChanged(event: any): void {

this.pagination.currentPage = event.peage;
this.loadMessage();
  }
 
}
