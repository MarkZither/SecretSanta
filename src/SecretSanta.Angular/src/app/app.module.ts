import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SantaComponent } from './santa/santa.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthService } from '@app/core/services/auth.service';
import { LoginComponent } from './login/login.component';
import { EnvServiceProvider } from '@app/shared/env.service.provider';
import { ConcreteSantaService } from '@app/shared/santa'

import { SantaFactory } from './shared/santa.provider';

@NgModule({
  declarations: [
    AppComponent,
    SantaComponent,
    ProfileComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule
  ],
  providers: [AuthService, 
    EnvServiceProvider, 
    {provide: ConcreteSantaService, useFactory: SantaFactory}],
  bootstrap: [AppComponent]
})
export class AppModule { }
