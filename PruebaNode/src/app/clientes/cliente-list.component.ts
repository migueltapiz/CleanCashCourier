import { Component,OnDestroy, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { ICliente } from "./cliente";
import { ClienteService } from "./cliente.service";

@Component({
    selector : 'pm-clientes',
    templateUrl: './cliente-list.component.html',
    styleUrls: ['./cliente-list.component.css']
})
export class ClientesListComponent implements OnInit, OnDestroy{
    errorMessage: string = '';
    sub!: Subscription;
    filteredClientes: ICliente[] = [];
    clientes: ICliente[] = [];

    constructor(private clienteService: ClienteService) { }

    performFilter(filterBy: string): ICliente[] {
        filterBy = filterBy.toLocaleLowerCase();
        return this.clientes.filter((cliente: ICliente) =>
          cliente.nombre.toLocaleLowerCase().includes(filterBy));
      }
    
    ngOnInit(): void {
        this.sub = this.clienteService.getClientes().subscribe({
          next: clientes => {
            this.clientes = clientes;
            this.filteredClientes = this.clientes;
          },
          error: err => this.errorMessage = err
        });
      }
    
      ngOnDestroy(): void {
        this.sub.unsubscribe();
      }
}