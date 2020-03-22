import { Component, OnInit, Input , Output, EventEmitter } from '@angular/core';
import {AthuService} from '../_servise/auth.service';
import { AlertifyService } from '../_servise/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDaterangepickerConfig, BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../_models/User';

import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
user: User ;

@Output() cancelRrgister = new EventEmitter();
registerForm: FormGroup;
bsconfig: Partial <BsDatepickerConfig>;
  constructor( private router: Router, private fb: FormBuilder, private authservice: AthuService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.bsconfig = {
      containerClass: 'theme-red'
    },
    this.createRegistrationform();
  }

  createRegistrationform() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      city: ['', Validators.required],
      country : ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)] ],
      confirmPassword: ['', Validators.required]
          }, {validators : this.passwordMatchValidetior} );
  }
passwordMatchValidetior(g: FormGroup) {
return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};
}
  register() {
    if (this.registerForm.valid) {
this.user = Object.assign({}, this.registerForm.value);
this.authservice.register(this.user).subscribe(() => {
  
  this.alertify.success('registeration successful');
}, error => {
  this.alertify.error(error);
}, () => {
 this.authservice.login(this.user).subscribe(() => {
this.router.navigate(['/members']);

 });

  
})
;

    }

  }

  cancel() {
this.cancelRrgister.emit(false);
  }
}
