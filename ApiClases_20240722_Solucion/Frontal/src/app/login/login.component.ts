import { Component, OnInit } from '@angular/core';
import { UserService } from '../servicios/user.service';
import { Usuario } from '../clases/usuario';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  usuarios!: Usuario[];

  email: string = '';

  contrase: string = '';
password: any;
username: any;

  constructor(private usuarioService: UserService, private route: Router) { }

  authentication() {
    this.usuarioService.autenticarUsuario(this.email, this.contrase, true).subscribe(response => {
      if (response && response.token) {
        localStorage.setItem('token', response.token);
        alert("funciona");
        this.route.navigate(['/welcome']); // Redirige a la ruta protegida
      } else {
        alert('Credenciales incorrectas');
      }
    });
  }
  
  
}
