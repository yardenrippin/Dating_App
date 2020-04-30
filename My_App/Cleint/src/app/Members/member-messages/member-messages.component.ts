import { Component, OnInit, Input } from '@angular/core';

import { UserService } from 'src/app/_servise/User.service';
import { AthuService } from 'src/app/_servise/auth.service';
import { AlertifyService } from 'src/app/_servise/alertify.service';
import { Message } from 'src/app/_models/Message';
import { tap } from 'rxjs/operators';
import { TabHeadingDirective } from 'ngx-bootstrap';
import { currentId } from 'async_hooks';



@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
@Input() recipientid: number ;
messages: Message[];
newMessage: any = {};
  constructor(private userservis: UserService, private authService: AthuService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadMessages() ;
  }

loadMessages() {
  const userid = +this.authService.decoodedToken.nameid;
this.userservis.getMessageTread(this.authService.decoodedToken.nameid, this.recipientid).pipe(
  tap(messages =>{
    for (let i= 0; i < messages.length; i++) {
      if (messages[i].isRead === false && messages[i].recipientId === userid )
    this.userservis.MarkAsRead(userid,messages[i].id)
    }
  })
).subscribe( Message  => {

this.messages  = Message;

}, error => {this.alertify.error(error);});

}
sendMessage() {
this.newMessage.recipientid = this.recipientid;
this.userservis.sendmessage(this.authService.decoodedToken.nameid, this.newMessage ).subscribe((message: Message) => {
  this.messages.unshift(message);
  this.newMessage.content = '';
}, error => {this.alertify.error(error); } );

}

}
