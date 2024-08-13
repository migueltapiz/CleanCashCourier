import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WelcomeComponentComponent } from './welcome-component/welcome-component.component';
import { AboutComponent } from './about/about.component';
import { SendMoneyComponent } from './send-money/send-money.component';
import { TransaccionComponent } from './transaccion/transaccion.component';
import { RegistroComponent } from './registro/registro.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '', component:WelcomeComponentComponent},
  { path: 'about', component: AboutComponent },
  { path: 'registro', component: RegistroComponent },
  { path: 'login', component: LoginComponent },
  { path: 'send-money', component: SendMoneyComponent },
  { path: 'transaction-history', component: TransaccionComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), FormsModule,
    ReactiveFormsModule],
  exports: [RouterModule],
})
export class AppRoutingModule { }
