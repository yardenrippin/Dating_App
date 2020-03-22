import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/User';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_servise/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_servise/User.service';
import { AthuService } from 'src/app/_servise/auth.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  user: User;
  photourl: string;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
if (this.editForm.dirty) {
  $event.returnValue = true;
}
  }

  constructor(private route: ActivatedRoute , private alertify: AlertifyService,
     private userservice: UserService, private authserice: AthuService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
    this.authserice.currentPhotoUrl.subscribe(photourl=>this.photourl=photourl)
  }

  uspdateuser() {
this.userservice.updateUser(this.authserice.decoodedToken.nameid, this.user).subscribe(next => {
    this.alertify.success('profile update succesfuly');
    this.editForm.reset(this.user);
  }, error => {
    this.alertify.error(error);
  }
);

  }
  updateMainPhoto(Photourl) {
    this.user.photourl = Photourl;
  }

}
