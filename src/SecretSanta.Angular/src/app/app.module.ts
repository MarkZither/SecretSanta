import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SantaComponent } from './santa/santa.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthService } from '@app/core/services/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    SantaComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
