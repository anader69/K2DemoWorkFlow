import { BrowserModule } from '@angular/platform-browser';
import { NgModule, inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './Login/login.component';
import { InboxComponent } from './inbox/inbox.component';
import { LoaderInterceptor } from './loader-interceptor';
export const authGuard = () => {
  const router = inject(Router);
  var user = localStorage.getItem('user');
  if (user) {
    return true;
  }
  // Redirect to the login page
  return router.parseUrl('/login');
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    InboxComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [authGuard] },
      { path: 'home', component: HomeComponent, canActivate: [authGuard]},
      { path: 'login', component: LoginComponent },
      { path: 'count', component: CounterComponent },
      { path: 'auth', component: CounterComponent },
      { path: 'inbox', component: InboxComponent, canActivate: [authGuard] }
      
    ])
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
