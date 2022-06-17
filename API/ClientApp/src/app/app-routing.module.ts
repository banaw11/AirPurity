import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CityComponent } from './components/city/city.component';
import { EmailConfirmationComponent } from './components/email-confirmation/email-confirmation.component';
import { HomeComponent } from './components/home/home.component';
import { StationComponent } from './components/station/station.component';
import { StopNotificationComponent } from './components/stop-notification/stop-notification.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'city/:name', component: CityComponent},
  {path: 'station', component: StationComponent},
  {path: 'stop-notification', component: StopNotificationComponent},
  {path: 'email-confirmation', component: EmailConfirmationComponent},
  {path: '**', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
