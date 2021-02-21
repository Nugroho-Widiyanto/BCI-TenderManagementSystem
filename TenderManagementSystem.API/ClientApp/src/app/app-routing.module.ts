import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListTendersComponent } from './tendermanagement/list-tenders/list-tenders.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: ListTendersComponent,
  }
  , { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }