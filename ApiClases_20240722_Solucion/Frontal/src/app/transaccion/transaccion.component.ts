import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts';
import { TransaccionService } from '../servicios/transaccion.service';
import { Transaccion, TransaccionTabla } from '../interfaces/transaccion';
import { ClienteService } from '../servicios/cliente.service';
import { format } from 'date-fns';
import { Subscription } from 'rxjs';
import { ChartData, ChartOptions } from 'chart.js';

@Component({
  selector: 'pm-transaccion',
  templateUrl: './transaccion.component.html',
  styleUrls: ['./transaccion.component.css']
})
export class TransaccionComponent implements OnInit, OnDestroy {
  @ViewChild(BaseChartDirective) chart!: BaseChartDirective;

  transacciones: Transaccion[] = [];
  transaccionesTabla: TransaccionTabla[] = [];
  fechaInicio: string | null = null;
  fechaFin: string | null = null;
  cantidadMin: number | null = null;
  cantidadMax: number | null = null;

  lineChartData: ChartData<'line'> = {
    labels: [],  // Etiquetas para el eje X (fechas)
    datasets: [
      {
        data: [],  // Datos del gráfico
        label: 'Balance Diario',
        fill: false,
        borderColor: 'rgba(75,192,192,1)',  // Color de la línea
        tension: 0.1  // Curvatura de la línea
      }
    ],

  };

  lineChartOptions: ChartOptions<'line'> = {
    responsive: true,
    plugins: {
      legend: {
        display: true,
      }
    },
    scales: {
      x: {
        type: 'time',
        time: {
          unit: 'day'
        }
      },
      y: {
        beginAtZero: true
      }
    }
  };

  subcliente!: Subscription;
  subTransaccion!: Subscription;
  identificaciorCliente!: number;
  errorMessage: string = '';
  token!: string;

  constructor(private transaccionService: TransaccionService, private clienteService: ClienteService) { }

  ngOnInit(): void {
    this.token = localStorage['token'];

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

    this.lineChartData.labels = Object.keys(balanceDiario).sort();
    this.lineChartData.datasets[0].data = (this.lineChartData.labels as string[]).map((fecha: string) => balanceDiario[fecha]);


    if (this.chart) {
      this.chart.update();
    }
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
