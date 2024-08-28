import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormBuilder, FormGroup, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './about/about.component';
import { SendMoneyComponent } from './send-money/send-money.component';
import { TransaccionComponent } from './transaccion/transaccion.component';
import { RegistroComponent } from './registro/registro.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { CabeceraComponent } from './cabecera/cabecera.component';
import { WelcomeComponentComponent } from './welcome-component/welcome-component.component';
import { ContactsTableComponent } from './contacts-table/contacts-table.component';

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    SendMoneyComponent,
    TransaccionComponent,
    RegistroComponent,
    LoginComponent,
    ProfileComponent,
    CabeceraComponent,
    WelcomeComponentComponent,
    ContactsTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})


export class AppModule { }
