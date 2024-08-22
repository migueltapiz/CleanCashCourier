import { Component, OnInit } from '@angular/core';
import { ClienteService } from "../clientes/cliente.service";
import { ICliente } from "../clientes/cliente";
import { DatosService } from "../datos/datos.service";
import { IPais, PaisService } from '../servicios/pais.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'pm-profile-component',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  errorMessage: string = '';

  cliente: ICliente | undefined;
  isEditing = {
    usuario: false,
    pais: false,
    trabajo: false
  };
  isModified = false;
  isAnyFieldEdited = false;
  paises: string[] = [];
  trabajos: string[] = [];
  subPaises!: Subscription;
  subClientes!: Subscription;

  constructor(private clienteService: ClienteService,private paisService:PaisService, private datosService: DatosService) { }

  ngOnInit(): void {
    this.subClientes = this.clienteService.getClienteById(1).subscribe({
      next: (data) => this.cliente = data,
      error: (err) => console.error(err)
    });

    this.subPaises = this.paisService.getNombresPaises().subscribe({
      next: paises => {
        this.paises = paises;
       
      },
      error: err => this.errorMessage = err
        
    });

    //this.paises = this.datosService.getPaises();
    this.trabajos = this.datosService.getTrabajos();
  }

  enableEdit(field: keyof ICliente): void {
    if (field in this.isEditing) {
      this.isEditing[field as keyof typeof this.isEditing] = true;
    }
  }

  disableEdit(field: keyof ICliente): void {
    if (field in this.isEditing) {
      this.isEditing[field as keyof typeof this.isEditing] = false;
    }
  }

  onFieldChange(): void {
    this.isModified = true;
  }

  saveChanges(): void {
    if (this.cliente && this.isModified) {
      this.clienteService.updateCliente(this.cliente.id, this.cliente).subscribe({
        next: (data) => {
          this.cliente = data;
          this.isModified = false;
          alert('Cambios guardados exitosamente.');
        },
        error: (err) => console.error(err)
      });
    }
  }
}
