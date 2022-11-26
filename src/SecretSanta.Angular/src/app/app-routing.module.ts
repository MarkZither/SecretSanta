import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from '@app/profile/profile.component';
import { SantaComponent } from '@app/santa/santa.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  { path: 'santa', component: SantaComponent },
  { path: 'profile', 
      component: ProfileComponent, 
      canActivate: [AuthGuard], 
    data: {
      authGuardRedirect: '/'
    } 
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
