import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_servise/User.service';
import { AlertifyService } from 'src/app/_servise/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { TabsetComponent } from 'ngx-bootstrap';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild ('memberTabe', {static: true}) memberTabe: TabsetComponent;
user: User;

galleryOptions: NgxGalleryOptions [];
galleryImages: NgxGalleryImage [];
constructor( private route: ActivatedRoute, private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
 this.route.data.subscribe(data => {
   this.user = data['user']
 });

 this.route.queryParams.subscribe(params => {

const selectedTab = params['tab'];

this.memberTabe.tabs[selectedTab > 0 ? selectedTab : 0].active = true;
 })

 this.galleryOptions = [
   {
     width: '500px',
     height: '500px',
     imagePercent: 100,
     thumbnailsColumns: 4,
     imageAnimation: NgxGalleryAnimation.Slide,
     preview: false
   }
 ];
 this.galleryImages = this.getImages();

  }

getImages(){
  const imageUrls = [];
  for (const photo of this.user.photos) {
imageUrls.push({
small: photo.url,
medium: photo.url,
big: photo.url,
description: photo.description
});
  }
  return imageUrls;
}
selectTab(tabid: number) {
  this.memberTabe.tabs[tabid].active = true;
}

}
