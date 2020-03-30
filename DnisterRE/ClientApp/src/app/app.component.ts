import { Component, NgModule, ViewChild } from '@angular/core';
import { MatBottomSheet, MatFormField, MatDialogConfig, MatDialog } from '@angular/material'
import { LoginFormComponent } from './login-form/login-form.component'
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UsersService, User } from '../app/services/organization.service';
import { EditFromComponent } from './edit-from/edit-from.component';
import { UsersPageComponent } from './users-page/users-page.component';

export interface DialogData {
  login: string;
  password: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [UsersPageComponent]
})

export class AppComponent {
  title = 'new title';

  constructor(private bottomShit: MatBottomSheet,
    public dialog: MatDialog,
    private usersService: UsersService,
    public usersComponent: UsersPageComponent,
  ) { }

  openLoginDialog() {
    this.bottomShit.open(LoginFormComponent);
    console.log('#')
  }
}
