
// src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ErrorComponent } from './error/error.component';
import { OrganizationComponent } from './organization/organization.component';
import { HomePageComponent } from './home-page/home-page.component';
import { UsersPageComponent } from './users-page/users-page.component';

const routes: Routes = [
    { path: 'error', component: ErrorComponent },
    {
        path: 'api', component: OrganizationComponent, children: [
            { path: 'users', component: UsersPageComponent }
        ]
    },
    { path: '', component: HomePageComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
