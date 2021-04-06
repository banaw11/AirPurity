import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CityComponent } from './components/city/city.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'city', component: CityComponent},
  {path: '**', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
