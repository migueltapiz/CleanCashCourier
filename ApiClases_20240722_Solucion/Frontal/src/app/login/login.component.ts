import { Component } from '@angular/core';
import { Usuario } from '../interfaces/usuario';
import { Router } from '@angular/router';
import { ClienteService } from '../servicios/cliente.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';

  contrase: string = '';
  password: any;
  username: any;

  constructor(private clienteService: ClienteService, private route: Router) { }

  authentication() {
    this.clienteService.autenticarUsuario(this.email, this.contrase, true).subscribe(response => {
      if (response && response.token) {
        localStorage.setItem('token', response.token);
        this.route.navigate(['/welcome']); // Redirige a la ruta protegida
      } else {
        alert('Credenciales incorrectas');
        console.log(response);
      }
    });
  }
  //this.clienteService.registrarCliente(clienteRegistro).subscribe(
  //  next => {
  //    this.router.navigate(['/login']);
  //  },
  //  error => {
  //    console.log(error);
  //    console.log(error.error.message)
  //    console.error(error);
  //    if (error.status === 400) {
  //      console.error('Error de validación', error);
  //      alert('Error al registrar usuario: ' + error.error);
  //    } else if (error.status === 409) {
  //      alert('El email ya existe en CleanCashCourier, intentelo de nuevo');
  //    } else if (error.status === 0) {
  //      alert('No se pudo conectar al servidor.');
  //    } else {
  //      console.error(`Error ${error.status}: ${error.message} --- ${error.error.message}`);
  //      alert('Error en el proceso de registro: ' + error.message);
  //    }
  //  },

  //);
  
}
