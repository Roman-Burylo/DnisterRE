import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
import { MatBottomSheetModule } from '@angular/material/bottom-sheet'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { LoginFormComponent } from './login-form/login-form.component'
import { from } from 'rxjs';
//import { Lol2Component } from './lol2/lol2.component';
import { ErrorComponent } from './error/error.component'
import { GlobalErrorHandler } from './global-error-handler.service';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  imports: [
    MatFormField,
    MatInput,
    MatLabel,
    AppRoutingModule
    
  ],
  exports: [
    MatFormField,
    MatInput,
    MatLabel,
  ],
  declarations: [
    LoginFormComponent,
    //Lol2Component,
    ErrorComponent
    
  ]
}) export class MaterialModule { };

@NgModule({
  declarations: [
    AppComponent,
    LoginFormComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatToolbarModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatBottomSheetModule,
    AppRoutingModule
    // MatFormField,
    // MatInput,
    // MatLabel,
  ],
  providers: [
    {provide: ErrorHandler, useClass: GlobalErrorHandler}
  ],
  entryComponents: [LoginFormComponent],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
  ],
})
export class AppModule { }

