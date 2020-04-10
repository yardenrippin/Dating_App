import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/User';
import { AthuService } from 'src/app/_servise/auth.service';
import { UserService } from 'src/app/_servise/User.service';
import { AlertifyService } from 'src/app/_servise/alertify.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
@Input() user: User;
  constructor( private authSrvice: AthuService, private userservice: UserService, private alertify: AlertifyService) { }

  ngOnInit() {

  }
sendlike(id: number){

this.userservice.sendlike(this.authSrvice.decoodedToken.nameid, id).subscribe(data=>{
  this.alertify.success('you have liked: ' + this.user.knownAs);
}, error => {
  this.alertify.error(error);
} );

}
}
