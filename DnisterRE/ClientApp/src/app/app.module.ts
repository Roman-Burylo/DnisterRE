import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
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
import { from } from 'rxjs'

@NgModule({
  imports: [
    MatFormField,
    MatInput,
    MatLabel,
  ],
  exports: [
    MatFormField,
    MatInput,
    MatLabel,
  ],
  declarations: [
    LoginFormComponent,
  ]
}) export class MaterialModule { };

@NgModule({
  declarations: [
    AppComponent,
    LoginFormComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatToolbarModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatBottomSheetModule,
    // MatFormField,
    // MatInput,
    // MatLabel,
  ],
  providers: [],
  entryComponents: [LoginFormComponent],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
  ],
})
export class AppModule { }

