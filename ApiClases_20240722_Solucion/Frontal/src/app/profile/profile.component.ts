import { Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ClienteService } from "../clientes/cliente.service";
import { ICliente } from "../clientes/cliente";
import { DatosService } from "../datos/datos.service";
import { IPais, PaisService } from '../servicios/pais.service';
import { Subscription } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'pm-profile-component',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})


export class ProfileComponent implements OnInit {

  @ViewChild('dropdownMenuPais') dropdownMenu!: ElementRef;
  @ViewChildren('paisItem') paisItems!: QueryList<ElementRef>;

  @ViewChild('dropdownMenuEmpleo') dropdownMenuEmpleos!: ElementRef;
  @ViewChildren('empleoItem') empleoItems!: QueryList<ElementRef>;
  errorMessage: string = '';
  perfilForm!: FormGroup;

  cliente!: ICliente;
  isEditing = {
    usuario: false,
    pais: false,
    trabajo: false
  };
  isModified = false;
  isAnyFieldEdited = false;
  
  trabajos: string[] = [];
  subPaises!: Subscription;
  subClientes!: Subscription;
  isDropdownPaisesVisible: boolean = false;
  isDropdownEmpleosVisible: boolean = false;
  paises: IPais[] = [];
  paisesFiltrado: IPais[] = [];
  paisSeleccionado: number = -1;
  empleos: string[] = [];
  empleosFiltrados: string[] = [];

  constructor(private fb: FormBuilder, private clienteService: ClienteService,private paisService:PaisService, private datosService: DatosService) { }
  showDropdownPais() {
    this.isDropdownPaisesVisible = true;
  }

  hideDropdownPais() {
    this.isDropdownPaisesVisible = false;
  }
  showDropdownEmpleo() {
    this.isDropdownEmpleosVisible = true;
  }

  hideDropdownEmpleo() {
    this.isDropdownEmpleosVisible = false;
  }
  ngOnInit(): void {
    this.subClientes = this.clienteService.getClienteById(1).subscribe({
      next: (data) => this.cliente = data,
      error: (err) => console.error(err)
    });
    console.log(this.cliente);

    this.subPaises = this.paisService.getPaises().subscribe({
      next: paises => {
        this.paises = paises;
        this.paisesFiltrado = paises;
      },
      error: err => this.errorMessage = err
    });
    this.empleos = this.datosService.getTrabajos();
    this.empleosFiltrados = this.datosService.getTrabajos();

    //this.paises = this.datosService.getPaises();
    this.trabajos = this.datosService.getTrabajos();
    this.perfilForm = this.fb.group({
      Nombre: ['', [Validators.required, Validators.minLength(3)]],
      Apellido: ['', [Validators.required, Validators.minLength(3)]],
      Correo: ['', [Validators.required, Validators.email]],
      Contraseña: ['', [Validators.required, Validators.minLength(6)]],
      Contraseña2: ['', [Validators.required]],
      Rol: ['Client', Validators.required],
      PaisNombre: ['', Validators.required],
      Empleo: [''],
      FechaNac: ['', Validators.required]
    }, {
      //validator: this.passwordMatchValidator('Contraseña', 'Contraseña2')
    });
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
  onSubmit(): void {

  }
  selectPais(paisNombre: string) {
    //this.registroForm.value.PaisNombre = paisNombre;
    this.perfilForm.patchValue({ PaisNombre: paisNombre })
    //this.listPaisesFilter = paisNombre;

    var resultado = this.paises.find(pais => pais.nombre === paisNombre)?.id;
    if (resultado != undefined) {
      this.paisSeleccionado = resultado;
      this.hideDropdownPais();
    }
  }
  selectEmpleo(empleoNombre: string) {
    // Actualiza la propiedad correspondiente en tu formulario o donde sea necesario
    this.perfilForm.patchValue({ Empleo: empleoNombre });
    this.hideDropdownEmpleo();
    // Busca el empleo por nombre y obtén su ID (ajusta esto según tu estructura de datos)
    
    
  }





  filterPaises(event: Event) {
    const query = (event.target as HTMLInputElement).value.toLowerCase();
    this.paisesFiltrado = this.paises.filter(pais =>
      pais.nombre.toLowerCase().startsWith(query)
    );

    // Si hay un elemento coincidente, desplazar la vista hacia él
    if (this.paisesFiltrado.length > 0) {
      setTimeout(() => {
        const firstMatch = this.paisItems.first;
        if (firstMatch) {
          firstMatch.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
        }
      }, 0);
    }

  }
  filterEmpleos(event: Event) {
    const query = (event.target as HTMLInputElement).value.toLowerCase();
    this.empleosFiltrados = this.empleos.filter(empleo =>
      empleo.toLowerCase().startsWith(query)
    );

    // Si hay un elemento coincidente, desplazar la vista hacia él
    if (this.empleosFiltrados.length > 0) {
      setTimeout(() => {
        const firstMatch = this.empleoItems.first;
        if (firstMatch) {
          firstMatch.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
        }
      }, 0);
    }
  }

    
}
