import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SantaComponent } from './santa/santa.component';

const routes: Routes = [
  { path: 'santa', component: SantaComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
