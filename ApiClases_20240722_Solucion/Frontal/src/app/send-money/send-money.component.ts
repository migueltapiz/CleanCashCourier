import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { ICliente } from "../clientes/cliente";
import { ClienteService } from "../clientes/cliente.service";
import { TransaccionService } from "../transaccion/transaccion.service";
import { ITransaccion, Transaccion } from "../transaccion/transaccion";
import { Router } from '@angular/router';

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
  selectedCliente: ICliente | null = null;
  clienteEnvia!: ICliente;
  modalMessage: string = '';
  transaccion!: Transaccion;

  constructor(private clienteService: ClienteService, private transaccionService: TransaccionService, private router: Router) { }

  ngOnInit(): void {
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

  performFilter(filterBy: string): ICliente[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.clientes.filter((cliente: ICliente) =>
      cliente.usuario.toLocaleLowerCase().includes(filterBy));
  }

  selectCliente(cli: ICliente) {
    this.selectedCliente = cli;
    this._listFilter = cli.usuario;
    this.filteredClientes = [];
  }

  recipientFilter = '';
  amount: string = '';
  currency: string = 'EUR';

  validateAmount(event: Event) {
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

    this.amount = value;
    input.value = value;
  }

  isValidAmount(): boolean {
    const numericAmount = parseFloat(this.amount.replace(',', '.'));
    return numericAmount >= 0.5 && !!this.selectedCliente;
  }

  crearTransaccion() {
    this.transaccion = new Transaccion();

    this.transaccion.cantidadEnvia = parseFloat(this.amount.replace(',', '.'));
    this.transaccion.cantidadRecibe = parseFloat(this.amount.replace(',', '.'));
    this.transaccion.idEnvia = 1; // Ejemplo de ID del cliente que envía, esto debería configurarse adecuadamente
    this.transaccion.idRecibe = this.selectedCliente?.id || 0;
    this.transaccion.fecha = new Date(Date.now()).toJSON();
  }

  sendMoney() {
    if (!this.selectedCliente) {
      this.modalMessage = 'Por favor, seleccione un destinatario válido.';
      this.showModal();
      return;
    }

    if (!this.isValidAmount()) {
      this.modalMessage = 'La cantidad mínima a enviar es 0,5.';
      this.showModal();
      return;
    }

    this.crearTransaccion();

    this.transaccionService.crearTransaccion(this.transaccion).subscribe({
      next: (data) => {
        console.log('Transacción creada: ' + JSON.stringify(data));
      },
      error: (err) => {
        console.error('Error creando la transacción: ', err);
      }
    });

    const numericAmount = parseFloat(this.amount.replace(',', '.'));
    this.modalMessage = `Enviando ${numericAmount.toFixed(2)} ${this.currency} al destinatario ${this.selectedCliente.usuario}.`;
    this.showModal();
  }

  showModal() {
    const modalElement = document.getElementById('transactionModal');
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
  }

  navegarATransacciones() {
    this.router.navigate(['/transaction-history'])
  }
}
