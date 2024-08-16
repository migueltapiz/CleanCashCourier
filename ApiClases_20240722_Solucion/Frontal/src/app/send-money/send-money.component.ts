/* send-money.component.ts */
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { ICliente } from "../clientes/cliente";
import { ClienteService } from "../clientes/cliente.service";
import { TransaccionService } from "../transaccion/transaccion.service";
import { ITransaccion, Transaccion } from "../transaccion/transaccion";
import { Router } from '@angular/router';
import { PaisService } from '../servicios/pais.service';

declare var bootstrap: any;

@Component({
  selector: 'pm-send-money',
  templateUrl: './send-money.component.html',
  styleUrls: ['./send-money.component.css']
})
export class SendMoneyComponent implements OnInit, OnDestroy {
  errorMessage: string = '';
  sub!: Subscription;
  filteredClientes: ICliente[] = [];
  clientes: ICliente[] = [];
  selectedCliente: ICliente | undefined = undefined;
  clienteEnvia: ICliente = new ICliente();
  modalMessage: string = '';
  transaccion!: Transaccion;
  

  recipientFilter: string = '';
  cantidadEnviada: string = '';
  cantidadRecibida: string = '';
  currencyEnviada: string = 'EUR';
  currencyRecibida: string = 'USD'; // Moneda por defecto para recibir
  factorConversion: number = 1.1; // Factor de conversión estático, debería obtenerse dinámicamente
  tarifaTransferencia: number = 0.00; // Tarifa de transferencia fija por ahora
  totalTransferencia: string = '0.00';

  constructor(private clienteService: ClienteService, private transaccionService: TransaccionService,private paisService: PaisService, private router: Router) { }

  ngOnInit(): void {
    this.clienteEnvia.id = 1;
    this.paisService.getPaisId(this.clienteEnvia.id).subscribe({
      next: pais => {
        this.currencyEnviada = pais.iso3;
      },
      error: err => this.errorMessage = err
    });
    this.sub = this.clienteService.getClientes().subscribe({
      next: clientes => {
        this.clientes = clientes;
        this.filteredClientes = [];
      },
      error: err => this.errorMessage = err
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  validateAmount(event: Event, isEnviada: boolean) {
    const input = event.target as HTMLInputElement;
    let value = input.value;

    // Replace commas with periods and remove invalid characters
    value = value.replace(/,/g, '.').replace(/[^0-9.]/g, '');

    const parts = value.split('.');
    if (parts.length > 2) {
      value = parts[0] + '.' + parts.slice(1).join('');
    }

    if (value.startsWith('.')) {
      value = '0' + value;
    }

    // Limit to two decimal places
    if (parts.length > 1 && parts[1].length > 2) {
      value = `${parts[0]}.${parts[1].substring(0, 2)}`;
    }

    if (isEnviada) {
      this.cantidadEnviada = value;
      this.cantidadRecibida = value ? (parseFloat(value) * this.factorConversion).toFixed(2) : '';
    } else {
      this.cantidadRecibida = value;
      this.cantidadEnviada = value ? (parseFloat(value) / this.factorConversion).toFixed(2) : '';
    }

    // Calcular el total de la transferencia
    this.totalTransferencia = (parseFloat(this.cantidadEnviada) + this.tarifaTransferencia).toFixed(2);

    input.value = value;
  }

  crearTransaccion() {
    this.transaccion = new Transaccion();

    this.transaccion.cantidadEnvia = parseFloat(this.cantidadEnviada.replace(',', '.'));
    this.transaccion.cantidadRecibe = parseFloat(this.cantidadRecibida.replace(',', '.'));
    this.transaccion.idEnvia = this.clienteEnvia.id;
    this.transaccion.idRecibe = this.selectedCliente?.id === null ? 0 : this.selectedCliente?.id || 0;
    this.transaccion.fecha = new Date(Date.now()).toJSON();
    console.log(this.transaccion);
  }

  sendMoney() {
    // Validación de coincidencia exacta del usuario
    this.selectedCliente = this.clientes.find(cliente => cliente.usuario === this.recipientFilter.trim());

    if (!this.selectedCliente) {
      this.showInvalidUserModal();
      return;
    }

    const numericAmount = parseFloat(this.cantidadEnviada.replace(',', '.'));
    if (!this.cantidadEnviada || this.cantidadEnviada.trim().length === 0) {
      this.showInvalidAmountModal();
      return;
    } else if (numericAmount < 0.5) {
      this.showAmountTooLowModal();
      return;
    }

    this.crearTransaccion();
    this.transaccionService.crearTransaccion(this.transaccion).subscribe({
      next: (transaccion) => {
        console.log(`Transacción creada: ` + JSON.stringify(transaccion));
      },
      error: (err) => {
        console.error('Error creando la transacción: ', err);
      }
    });

    const user = this.selectedCliente.usuario === undefined ? 'unknown' : this.selectedCliente.usuario;
    this.modalMessage = `Enviando ${numericAmount.toFixed(2)} ${this.currencyEnviada} al destinatario ${user}.`;
    this.showModal();
  }

  showInvalidUserModal() {
    const modalElement = document.getElementById('invalidUserModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
  }

  showInvalidAmountModal() {
    const modalElement = document.getElementById('invalidAmountModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
  }

  showAmountTooLowModal() {
    const modalElement = document.getElementById('amountTooLowModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
  }

  showModal() {
    const modalElement = document.getElementById('transactionModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
  }

  navegarATransacciones() {
    this.router.navigate(['/transaction-history']);
  }
}



// Se comenta el listFilter y su lógica de filtrado en tiempo real
/*
private _listFilter: string = '';
get listFilter(): string {
  return this._listFilter;
}
set listFilter(value: string) {
  this._listFilter = value;
  this.filteredClientes = this.performFilter(value);

  // Invalidar el cliente seleccionado si el texto no coincide exactamente
  if (this.selectedCliente && this.selectedCliente.usuario !== value) {
    this.selectedCliente = null;
  }

  // Restablecer selectedCliente si se borra el texto o no hay coincidencias
  if (this._listFilter === '' || this.filteredClientes.length === 0) {
    this.selectedCliente = null;
  }
}
*/

// El método performFilter ya no es necesario porque no filtramos en tiempo real
/*
performFilter(filterBy: string): ICliente[] {
  filterBy = filterBy.toLocaleLowerCase();
  return this.clientes.filter((cliente: ICliente) =>
    cliente.usuario.toLocaleLowerCase().includes(filterBy));
}
*/

// Se comenta la lógica de selección del cliente en la lista filtrada
/*
selectCliente(cli: ICliente) {
  this.selectedCliente = cli;
  this._listFilter = cli.usuario;
  this.filteredClientes = [];
}
*/
