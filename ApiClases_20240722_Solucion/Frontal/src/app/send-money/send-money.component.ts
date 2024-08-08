import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { ICliente } from "../clientes/cliente";
import { ClienteService } from "../clientes/cliente.service";
import { TransaccionService } from "../transaccion/transaccion.service";
import { ITransaccion,Transaccion } from "../transaccion/transaccion";

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
  selectedCliente!: ICliente;
  clienteEnvia!: ICliente;
  modalMessage: string = '';
  transaccion!: Transaccion;

  constructor(private clienteService: ClienteService,private transaccionService:TransaccionService) { }

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

    // Prevent multiple periods
    const parts = value.split('.');
    if (parts.length > 2) {
      value = parts[0] + '.' + parts.slice(1).join('');
    }

    // Ensure at least one digit before the period
    if (value.startsWith('.')) {
      value = '0' + value;
    }

    // Update the input value
    this.amount = value;

    // Update the input element value to ensure correct formatting
    input.value = value;
  }

  isValidAmount(): boolean {
    const numericAmount = parseFloat(this.amount.replace(',', '.'));
    return numericAmount >= 0.5;
  }

  crearTransaccion() {
    this.transaccion = new Transaccion;
   
    this.transaccion.cantidadEnvia = parseFloat(this.amount.replace(',', '.'));
    this.transaccion.cantidadRecibe = parseFloat(this.amount.replace(',', '.'));
    this.transaccion.idEnvia = 1;
    this.transaccion.idRecibe = this.selectedCliente.id;
    this.transaccion.fecha = new Date(Date.now()).toJSON();
  }

  sendMoney() {
    if (!this.selectedCliente) {
      this.modalMessage = 'El destinatario es inválido.';
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
        console.error('Error creando la transaccion: ', err);
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
}
