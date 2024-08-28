import { Component, OnDestroy, OnInit } from '@angular/core';
import { TransaccionService } from './transaccion.service';
import { Transaccion, TransaccionTabla } from './transaccion';
import { ICliente } from '../clientes/cliente';
import { ClienteService } from '../clientes/cliente.service';
import { format } from 'date-fns';
import {CabeceraComponent } from '../cabecera/cabecera.component'
import { Subscription } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'pm-transaccion',
  templateUrl: './transaccion.component.html',
  styleUrls: ['./transaccion.component.css']
})
export class TransaccionComponent implements OnInit,OnDestroy {
  transacciones: Transaccion[] = [];
  transaccionesTabla: TransaccionTabla[] = [];
  fechaInicio: string | null = null;
  fechaFin: string | null = null;
  cantidadMin: number | null = null;
  cantidadMax: number | null = null;

  subcliente!: Subscription;
  subTransaccion!: Subscription;

  cuantas: number = 0;
  constructor(private transaccionService: TransaccionService, private clienteService: ClienteService) { }

  cliente!: ICliente;
  errorMessage: string = '';
  token!: string;
  nombre!: string;

  ngOnInit(): void {
    this.token = localStorage['token'];

    const decoded = jwtDecode(this.token) as { [key: string]: any };

    this.nombre = decoded['sub'];

    this.subcliente = this.clienteService.getClienteByName(this.nombre).subscribe({
      next: cliente => {
        this.cliente = cliente;
        this.subTransaccion = this.transaccionService.getTransacciones(this.cliente.id).subscribe(
          async (data: Transaccion[]) => {
            this.transacciones = data;
            await Promise.all(this.transacciones.map(async (transaccion) => {
              const transaccionTabla = new TransaccionTabla();
              transaccionTabla.nombre = await this.obtenerIdUsuario(transaccion);
              transaccionTabla.cantidad = this.obtenerCantidad(transaccion);
              transaccionTabla.fecha = this.obtenerFecha(transaccion);
              transaccionTabla.tipo = this.obtenerTipoTransaccion(transaccion);
              this.transaccionesTabla.push(transaccionTabla);
            }));
          },
          error => console.error(error)
        );
        
      },
      error: err => this.errorMessage = err
    });
  }
  ngOnDestroy(): void {
    this.subcliente?.unsubscribe();
    this.subTransaccion?.unsubscribe();
  }

 

  obtenerIdUsuario(transaccion: Transaccion): Promise<string> {
    console.log(transaccion);
    // Si la transacción es recibida (idRecibe es 1), muestra el idEnvia, de lo contrario, muestra idRecibe
    var clienteIdAux = transaccion.idRecibe === this.cliente.id ? transaccion.idEnvia : transaccion.idRecibe;
    return new Promise((resolve, reject) => {
      this.subcliente = this.clienteService.getClienteById(clienteIdAux).subscribe({
        next: cliente => {
          resolve(cliente.usuario);
        },
        error: err => {
          this.errorMessage = err;
          reject('Unknow');
        }
      });
    });
    
  }

  obtenerCantidad(transaccion: Transaccion): number {
    return transaccion.idEnvia === 1 ? transaccion.cantidadEnvia : transaccion.cantidadRecibe;
  }

  obtenerTipoTransaccion(transaccion: Transaccion): string {
    return transaccion.idEnvia === this.cliente.id ? 'Enviada' : 'Recibida';
  }

  obtenerFecha(transaccion: Transaccion): string {
    return format(new Date(transaccion.fecha), 'dd/MM/yyyy');
  }

  actualizarFechaInicio(event: any): void {
    this.fechaInicio = event.target.value || null;
  }

  actualizarFechaFin(event: any): void {
    this.fechaFin = event.target.value || null;
  }

  actualizarCantidadMin(event: any): void {
    this.cantidadMin = event.target.value ? parseFloat(event.target.value) : null;
  }

  actualizarCantidadMax(event: any): void {
    this.cantidadMax = event.target.value ? parseFloat(event.target.value) : null;
  }

  borrarCantidadMin(): void {
    this.cantidadMin = null;
    const cantidadMinElement = document.getElementById('cantidadMin') as HTMLInputElement;
    if (cantidadMinElement) {
      cantidadMinElement.value = '';
    }
  }

  borrarCantidadMax(): void {
    this.cantidadMax = null;
    const cantidadMaxElement = document.getElementById('cantidadMax') as HTMLInputElement;
    if (cantidadMaxElement) {
      cantidadMaxElement.value = '';
    }
  }
  filtrarTransacciones(): void {
    // Aquí puedes construir los parámetros de la consulta con base en los filtros aplicados.
    // Luego, realiza la llamada al servicio para obtener las transacciones filtradas.
    // Por ejemplo:
    this.subTransaccion = this.transaccionService.getTransaccionesFiltradas({
      fechaInicio: this.fechaInicio,
      fechaFin: this.fechaFin,
      cantidadMin: this.cantidadMin,
      cantidadMax: this.cantidadMax
    }, this.cliente.id).subscribe(
      (data: Transaccion[]) => this.transacciones = data,
      error => console.error(error)
    );
  }


 
 
}
