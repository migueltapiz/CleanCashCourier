import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './about/about.component';
import { RouterModule } from '@angular/router';
import { WelcomeComponentComponent } from './welcome-component/welcome-component.component';
import { SendMoneyComponent } from './send-money/send-money.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    SendMoneyComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
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
