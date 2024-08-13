import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WelcomeComponentComponent } from './welcome-component/welcome-component.component';
import { AboutComponent } from './about/about.component';
import { SendMoneyComponent } from './send-money/send-money.component';
import { TransaccionComponent } from './transaccion/transaccion.component';

const routes: Routes = [
  { path: '', component:WelcomeComponentComponent},
  { path: 'about', component:AboutComponent},
  { path: 'send-money', component: SendMoneyComponent },
  { path: 'transaction-history', component: TransaccionComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
