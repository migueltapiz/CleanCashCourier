import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'pm-welcome-component',
  templateUrl: './welcome-component.component.html',
  styleUrl: './welcome-component.component.css'
})
export class WelcomeComponentComponent {
  constructor(private router: Router) { }

  navegarAEnvioDinero() {
    this.router.navigate(['/send-money'])
  }

  navegarARegistro() {
    this.router.navigate(['/registro'])
  }

}
