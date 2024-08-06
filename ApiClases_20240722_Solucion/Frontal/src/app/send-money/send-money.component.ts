import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { ICliente } from "../clientes/cliente";
import { ClienteService } from "../clientes/cliente.service";

@Component({
  selector: 'pm-send-money',
  templateUrl: './send-money.component.html',
  styleUrl: './send-money.component.css'
})
export class SendMoneyComponent implements OnInit, OnDestroy{
  errorMessage: string = '';
  sub!: Subscription;
  filteredClientes: ICliente[] = [];
  clientes: ICliente[] = [];
  selectedCliente?: string;
  clienteEnvia!: ICliente;
  constructor(private clienteService: ClienteService) { }

  
  
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

  selectCliente(cli: string) {
    this.selectedCliente = cli;
    this._listFilter = cli;
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

  sendMoney() {
    if (!this.selectCliente) {
      alert('El destinatario es inválido.');
      return;
    }

    if (!this.isValidAmount()) {
      alert('La cantidad mínima a enviar es 0,5.');
      return;
    }

    const numericAmount = parseFloat(this.amount.replace(',', '.'));
    alert(`Enviando ${numericAmount.toFixed(2)} ${this.currency} al destinatario ${this.selectedCliente}.`);
  }
}
