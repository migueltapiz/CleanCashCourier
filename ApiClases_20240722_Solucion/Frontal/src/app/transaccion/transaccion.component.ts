import { Component, OnDestroy, OnInit } from '@angular/core';
import { TransaccionService } from '../servicios/transaccion.service';
import { Transaccion, TransaccionTabla } from '../interfaces/transaccion';
import { ClienteService } from '../servicios/cliente.service';
import { format } from 'date-fns';
import { Subscription } from 'rxjs';
import * as Highcharts from 'highcharts';
import { Options } from 'highcharts';

@Component({
  selector: 'pm-transaccion',
  templateUrl: './transaccion.component.html',
  styleUrls: ['./transaccion.component.css']
})
export class TransaccionComponent implements OnInit, OnDestroy {
  Highcharts: typeof Highcharts = Highcharts;
  chartOptions: Options = {};
  transacciones: Transaccion[] = [];
  transaccionesTabla: TransaccionTabla[] = [];
  fechaInicio: string | null = null;
  fechaFin: string | null = null;
  cantidadMin: number | null = null;
  cantidadMax: number | null = null;

  subcliente!: Subscription;
  subTransaccion!: Subscription;
  identificaciorCliente!: number;
  errorMessage: string = '';
  token!: string;

  constructor(private transaccionService: TransaccionService, private clienteService: ClienteService) { }

  ngOnInit(): void {
    this.token = localStorage['token'];
    this.procesarTransacciones();

    this.subcliente = this.clienteService.getCliente(this.token).subscribe({
      next: cliente => {
        this.identificaciorCliente = cliente.id;
        this.subTransaccion = this.transaccionService.getTransacciones(this.identificaciorCliente).subscribe(
          async (data: Transaccion[]) => {
            this.transacciones = data;
            this.procesarTransacciones();
            await Promise.all(this.transacciones.map(async (transaccion) => {
              const transaccionTabla = new TransaccionTabla();
              transaccionTabla.nombre = await this.obtenerIdUsuario(transaccion);
              transaccionTabla.monto = this.obtenerMonto(transaccion);
              transaccionTabla.moneda = this.obtenerMoneda(transaccion);
              transaccionTabla.costeTransaccion = this.obtenerCosteTransaccion(transaccion);
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

  procesarTransacciones(): void {
    const balanceDiario: { [key: string]: number } = {};

    this.transacciones.forEach((transaccion) => {
      const fecha = format(new Date(transaccion.fecha), 'yyyy-MM-dd');
      const monto = this.obtenerMonto(transaccion);
      const tipo = this.obtenerTipoTransaccion(transaccion);

      const balanceCambio = tipo === 'Enviada' ? -monto : monto;

      if (!balanceDiario[fecha]) {
        balanceDiario[fecha] = 0;
      }
      balanceDiario[fecha] += balanceCambio;
    });

    const fechas = Object.keys(balanceDiario).sort();
    const datos = fechas.map((fecha) => balanceDiario[fecha]);

    this.chartOptions = {
      chart: {
        type: 'line' // Especifica el tipo de gráfico
      },
      title: {
        text: 'Balance Diario'
      },
      xAxis: {
        categories: fechas,
        title: {
          text: 'Fecha'
        }
      },
      yAxis: {
        title: {
          text: 'Monto'
        }
      },
      series: [
        {
          type: 'line', // Especifica el tipo de serie
          name: 'Balance Diario',
          data: datos
        }
      ]
    };
  }



  obtenerIdUsuario(transaccion: Transaccion): Promise<string> {
    const clienteIdAux = transaccion.idRecibe === this.identificaciorCliente ? transaccion.idEnvia : transaccion.idRecibe;
    return new Promise((resolve, reject) => {
      this.subcliente = this.clienteService.getClienteById(clienteIdAux).subscribe({
        next: cliente => {
          resolve(cliente.usuario);
        },
        error: err => {
          this.errorMessage = err;
          reject('Desconocido');
        }
      });
    });
  }

  obtenerMonto(transaccion: Transaccion): number {
    return transaccion.idEnvia === this.identificaciorCliente ? transaccion.cantidadEnvia : transaccion.cantidadRecibe;
  }

  obtenerMoneda(transaccion: Transaccion): string {
    return transaccion.idEnvia === this.identificaciorCliente ? transaccion.monedaOrigen : transaccion.monedaDestino;
  }

  obtenerCosteTransaccion(transaccion: Transaccion): number | null {
    return transaccion.idEnvia === this.identificaciorCliente ? transaccion.costeTransaccion : null;
  }

  obtenerFecha(transaccion: Transaccion): string {
    return format(new Date(transaccion.fecha), 'dd/MM/yyyy');
  }

  obtenerTipoTransaccion(transaccion: Transaccion): string {
    return transaccion.idEnvia === this.identificaciorCliente ? 'Enviada' : 'Recibida';
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
    this.subTransaccion = this.transaccionService.getTransaccionesFiltradas({
      fechaInicio: this.fechaInicio,
      fechaFin: this.fechaFin,
      cantidadMin: this.cantidadMin,
      cantidadMax: this.cantidadMax
    }, this.identificaciorCliente).subscribe(
      async (data: Transaccion[]) => {
        this.transaccionesTabla = [];
        await Promise.all(data.map(async (transaccion) => {
          const transaccionTabla = new TransaccionTabla();
          transaccionTabla.nombre = await this.obtenerIdUsuario(transaccion);
          transaccionTabla.monto = this.obtenerMonto(transaccion);
          transaccionTabla.moneda = this.obtenerMoneda(transaccion);
          transaccionTabla.costeTransaccion = this.obtenerCosteTransaccion(transaccion);
          transaccionTabla.fecha = this.obtenerFecha(transaccion);
          transaccionTabla.tipo = this.obtenerTipoTransaccion(transaccion);
          this.transaccionesTabla.push(transaccionTabla);
        }));

        this.procesarTransacciones();
      },
      error => console.error(error)
    );
  }
}
