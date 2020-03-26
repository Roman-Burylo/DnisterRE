import { Component, NgModule } from '@angular/core';
import { MatBottomSheet, MatFormField } from '@angular/material'
import { LoginFormComponent } from './login-form/login-form.component'
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

export interface DialogData {
  login: string;
  password: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {
  title = 'app';

  constructor(private bottomShit: MatBottomSheet) {

  }

  openLoginDialog() {
    this.bottomShit.open(LoginFormComponent);
    console.log('#')
  }
}
