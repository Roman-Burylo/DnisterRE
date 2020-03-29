import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import {
  MatToolbarModule,
  MatButtonModule,
  MatLabel,
  MatFormField,
  MatInput
} from '@angular/material';

import { MatDialogModule } from '@angular/material';
// import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog'

import { MatBottomSheetModule } from '@angular/material/bottom-sheet'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { LoginFormComponent } from './login-form/login-form.component'
import { from } from 'rxjs'

import { ErrorComponent } from './error/error.component'
import { GlobalErrorHandler } from './global-error-handler.service';
import { AppRoutingModule } from './app-routing.module';
import { NewsComponent } from './components/news/news.component';
import { OrganizationComponent } from './organization/organization.component';
import { HomePageComponent } from './home-page/home-page.component';
import { UsersPageComponent } from './users-page/users-page.component'
import { EditFromComponent } from './edit-from/edit-from.component';
import { AdduserFormComponent } from './adduser-form/adduser-form.component';

@NgModule({
  imports: [
    MatFormField,
    MatInput,
    MatLabel,
    AppRoutingModule,
    // MatDialog
  ],
  exports: [
    MatFormField,
    MatInput,
    MatLabel,
  ],
  declarations: [
    LoginFormComponent,
    NewsComponent,
    ErrorComponent,
    OrganizationComponent,
    HomePageComponent,
    UsersPageComponent,
    EditFromComponent
  ]
}) export class MaterialModule { };

@NgModule({
  declarations: [
    AppComponent,
    LoginFormComponent,
    ErrorComponent,
    OrganizationComponent,
    HomePageComponent,
    UsersPageComponent,
    EditFromComponent,
    AdduserFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatToolbarModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatBottomSheetModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatDialogModule,
  ],
  providers: [
    { provide: ErrorHandler, useClass: GlobalErrorHandler }
  ],
  entryComponents: [LoginFormComponent, EditFromComponent, AdduserFormComponent],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
  ],
})
export class AppModule { }

