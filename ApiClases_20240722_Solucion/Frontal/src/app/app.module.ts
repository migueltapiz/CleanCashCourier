import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {  FormBuilder, FormGroup,FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './about/about.component';
import { SendMoneyComponent } from './send-money/send-money.component';
import { TransaccionComponent } from './transaccion/transaccion.component';
import { RegistroComponent } from './registro/registro.component';
import { LoginComponent } from './login/login.component';


@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    SendMoneyComponent,
    TransaccionComponent,
    RegistroComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
    // RouterModule.forRoot([
    //   { path: 'about', component: AboutComponent, title: 'Acerca de Nosotros' },
    //   // { path: 'products/:id', component: ProductDetailComponent },
    //   { path: 'welcome', component: WelcomeComponentComponent },
    //   { path: '', redirectTo: 'welcome', pathMatch: 'full' },
    //   { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    // ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})


export class AppModule { }

