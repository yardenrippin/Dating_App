import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import {FileUploader} from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AthuService } from 'src/app/_servise/auth.service';
import { UserService } from 'src/app/_servise/User.service';
import { AlertifyService } from 'src/app/_servise/alertify.service';

@Component({
  selector: 'app-photo-edittor',
  templateUrl: './photo-edittor.component.html',
  styleUrls: ['./photo-edittor.component.css']
})
export class PhotoEdittorComponent implements OnInit {
@Input() photos: Photo[];
@Output() getMemberPhotoChange = new EventEmitter<string>();
uploader: FileUploader ;
mainPhto: Photo ;
hasBaseDropZoneOver = false;
baseurl = environment.apiUrl;


  constructor( private alertify: AlertifyService , private authService: AthuService, private userservic: UserService) { }

  ngOnInit() {
    this.initialzeUploader();
  }
fileOverBase(e: any): void {
  this.hasBaseDropZoneOver = e;
}

initialzeUploader() {
  this.uploader = new FileUploader({
url: this.baseurl + 'users/' + this.authService.decoodedToken.nameid + '/photos',
authToken: 'Bearer ' + localStorage.getItem('token'),
allowedFileType: ['image'],
removeAfterUpload: true,
autoUpload: false,
maxFileSize: 10 * 1024 * 1024

  });
  this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; };

  this.uploader.onSuccessItem = (item , response , status , headers) => {
    if (response) {
      const res: Photo = JSON.parse(response);
      const newphoto = {
        id: res.id,
        url: res.url,
        dAteAdded : res.dAteAdded,
        description: res.description,
        ismain: res.ismain
      };

      this.photos.push(newphoto);
      if(newphoto.ismain) {
        this.authService.changememberphoto(newphoto.url);
        this.authService.currentuser.photourl = newphoto.url;
        localStorage.setItem('user', JSON.stringify(this.authService.currentPhotoUrl));
      }
    }
  };
}
setmainphoto(photo: Photo) {
  this.userservic.setmainphoto(this.authService.decoodedToken.nameid, photo.id).subscribe(() => {
    this.mainPhto = this.photos.filter(p => p.ismain === true)[0];
    this.mainPhto.ismain = false;
    photo.ismain = true;
    this.authService.changememberphoto(photo.url);
    this.authService.currentuser.photourl = photo.url;
    localStorage.setItem('user', JSON.stringify(this.authService.currentPhotoUrl));
  }, error => {
    this.alertify.error(error);
  });
}
deletephoto(id: number) {
this.alertify.confirm('are you sure you want to dekete this photo?', () => { 
this.userservic.deletephoto(this.authService.decoodedToken.nameid, id).subscribe(() => {
  this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
  this.alertify.success ('photo has ben deleted');
}, error =>{
  this.alertify.error('filded to delete the photo');
})

});
}
}
