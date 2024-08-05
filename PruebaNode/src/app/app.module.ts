import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './about/about.component';
import { RouterModule } from '@angular/router';
import { WelcomeComponentComponent } from './welcome-component/welcome-component.component';
import { SendMoneyComponent } from './send-money/send-money.component';

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    SendMoneyComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'about', component: AboutComponent, title: 'Acerca de Nosotros' },
      // { path: 'products/:id', component: ProductDetailComponent },
      { path: 'welcome', component: WelcomeComponentComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
