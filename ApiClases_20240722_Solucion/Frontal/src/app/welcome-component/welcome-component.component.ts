import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CabeceraComponent } from '../cabecera/cabecera.component'
import jwt from 'jsonwebtoken';
import { jwtDecode } from "jwt-decode";
@Component({
  selector: 'welcome-component',
  templateUrl: './welcome-component.component.html',
  styleUrl: './welcome-component.component.css'
})
export class WelcomeComponentComponent implements OnInit{

  token: any;
  constructor(private router: Router) { }
    ngOnInit(): void {
      
    }

  navegarAEnvioDinero() {
    this.router.navigate(['/send-money'])
  }

  navegarARegistro() {
    this.router.navigate(['/registro'])
  }

}
