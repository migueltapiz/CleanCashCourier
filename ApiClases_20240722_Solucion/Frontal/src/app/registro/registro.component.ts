import { Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { RegistroCliente } from '../interfaces/registroCliente';
import { Router } from '@angular/router';
import { IPais, PaisService } from '../servicios/pais.service';
import { ClienteService } from '../servicios/cliente.service';
import { Observable, of, delay, map } from 'rxjs';
import { DatosService } from '../servicios/datos.service';
import Swal from 'sweetalert2';  // Importación de SweetAlert2

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  @ViewChild('dropdownMenu') dropdownMenu!: ElementRef;
  @ViewChildren('paisItem') paisItems!: QueryList<ElementRef>;
  @ViewChild('empleoDropdownMenu') empleoDropdownMenu!: ElementRef;
  @ViewChildren('empleoItem') empleoItems!: QueryList<ElementRef>;

  registroForm!: FormGroup;
  errorMessage: string | null = null; // Mensaje de error general
  paises: IPais[] = [];
  paisesFiltrado: IPais[] = [];
  empleos: string[] = [];
  empleosFiltrado: string[] = [];
  paisSeleccionado: number = -1;
  isDropdownVisible = false;
  isEmpleoDropdownVisible = false;

  constructor(
    private fb: FormBuilder,
    private datosService: DatosService,
    private clienteService: ClienteService,
    private paisService: PaisService,
    private router: Router
  ) { }

  showDropdown() {
    this.isDropdownVisible = true;
  }

  hideDropdown() {
    this.isDropdownVisible = false;
  }

  showEmpleoDropdown() {
    this.isEmpleoDropdownVisible = true;
  }

  hideEmpleoDropdown() {
    this.isEmpleoDropdownVisible = false;
  }

  ngOnInit(): void {
    this.paisSeleccionado = 1;
    this.registroForm = this.fb.group({
      Nombre: ['', [Validators.required, Validators.minLength(2)]],
      Apellido: ['', [Validators.required, Validators.minLength(2)]],
      Correo: ['', [Validators.required, Validators.email]],
      Contraseña: ['', [Validators.required, Validators.minLength(6)]],
      Contraseña2: ['', [Validators.required]],
      Rol: ['Client', Validators.required],
      PaisNombre: ['', Validators.required],
      Empleo: [''],
      FechaNac: ['', Validators.required, this.mayorDeEdadValidator()]
    }, {
      validator: this.passwordMatchValidator('Contraseña', 'Contraseña2')
    });

    this.paisService.getPaises().subscribe({
      next: paises => {
        this.paises = paises;
        this.paisesFiltrado = paises;
      },
      error: err => this.errorMessage = err
    });

    this.empleos = this.datosService.getTrabajos();
    this.empleosFiltrado = this.empleos;
  }

  passwordMatchValidator(password: string, confirmPassword: string) {
    return (formGroup: FormGroup) => {
      const passwordControl = formGroup.get(password);
      const confirmPasswordControl = formGroup.get(confirmPassword);

      if (confirmPasswordControl?.errors && !confirmPasswordControl.errors['passwordMismatch']) {
        return;
      }

      if (passwordControl?.value !== confirmPasswordControl?.value) {
        confirmPasswordControl?.setErrors({ passwordMismatch: true });
      } else {
        confirmPasswordControl?.setErrors(null);
      }
    };
  }

  onSubmit(): void {
    if (this.registroForm.invalid) {
      this.showValidationErrors();
      return;
    }

    if (this.registroForm.value.Contraseña !== this.registroForm.value.Contraseña2) {
      this.showErrorModal('Las contraseñas no coinciden.');
      return;
    }

    const fechaNac = new Date(this.registroForm.value.FechaNac);

    this.paisSeleccionado = this.paises.find(pais => pais.id === this.paisSeleccionado) == undefined ? -1 : this.paisSeleccionado;
    const clienteRegistro: RegistroCliente = {
      Nombre: this.registroForm.value.Nombre,
      Apellido: this.registroForm.value.Apellido,
      Email: this.registroForm.value.Correo,
      Contrasena: this.registroForm.value.Contraseña,
      PaisId: this.paisSeleccionado,
      Empleo: this.registroForm.value.Empleo,
      NombrePais: this.registroForm.value.PaisNombre,
      FechaNacimiento: fechaNac
    };

    this.clienteService.registrarCliente(clienteRegistro).subscribe(
      next => {
        this.showSuccessModal('Registro exitoso. Redirigiendo al inicio de sesión...', () => {
          this.router.navigate(['/login']);
        });
      },
      error => {
        console.log(error);
        if (error.status === 400) {
          this.showErrorModal('Error al registrar usuario: ' + error.error);
        } else if (error.status === 409) {
          this.showErrorModal('El email ya existe en CleanCashCourier, intentelo de nuevo');
        } else if (error.status === 0) {
          this.showErrorModal('No se pudo conectar al servidor.');
        } else {
          this.showErrorModal('Error en el proceso de registro: ' + error.message);
        }
      }
    );
  }

  private showValidationErrors(): void {
    let errorMessage = 'Por favor corrige los siguientes errores:\n';
    const controls = this.registroForm.controls;

    for (const key in controls) {
      if (controls.hasOwnProperty(key)) {
        const control = controls[key];
        if (control.invalid) {
          if (control.errors) {
            if (control.errors['required']) {
              errorMessage += `- El campo ${key} es requerido.\n`;
            }
            if (control.errors['minlength']) {
              errorMessage += `- El campo ${key} debe tener al menos ${control.errors['minlength'].requiredLength} caracteres.\n`;
            }
            if (control.errors['email']) {
              errorMessage += `- El campo ${key} debe ser un email válido.\n`;
            }
            if (control.errors['passwordMismatch']) {
              errorMessage += `- Las contraseñas no coinciden.\n`;
            }
            if (control.errors['menorDeEdad']) {
              errorMessage += `- Debes ser mayor de edad para registrarte.\n`;
            }
          }
        }
      }
    }

    if (errorMessage === 'Por favor corrige los siguientes errores:\n') {
      errorMessage = 'Hay errores en el formulario. Por favor, corrígelos antes de enviar.';
    }

    this.showErrorModal(errorMessage);
  }

  showErrorModal(message: string) {
    console.log(message);
    Swal.fire({
      icon: 'error',
      title: 'Error',
      text: message,
      confirmButtonText: 'Ok'
    });
  }

  showSuccessModal(message: string, callback: () => void) {
    Swal.fire({
      icon: 'success',
      title: 'Registro exitoso',
      text: message,
      confirmButtonText: 'Ok'
    }).then(result => {
      if (result.isConfirmed) {
        callback();
      }
    });
  }

  selectPais(paisNombre: string) {
    this.registroForm.patchValue({ PaisNombre: paisNombre });

    var resultado = this.paises.find(pais => pais.nombre === paisNombre)?.id;
    if (resultado != undefined) {
      this.paisSeleccionado = resultado;
      this.hideDropdown();
    }
  }

  filterPaises(event: Event) {
    const query = (event.target as HTMLInputElement).value;
    this.paisesFiltrado = this.paises.filter(pais =>
      pais.nombre.toLowerCase().startsWith(query.toLowerCase())
    );

    if (this.paisesFiltrado.length == 1) {
      setTimeout(() => {
        if (query === this.paisesFiltrado[0].nombre) {
          this.hideDropdown();
        }
      }, 0);
    } else if (this.paisesFiltrado.length > 1) {
      setTimeout(() => {
        const firstMatch = this.paisItems.first;
        if (firstMatch) {
          firstMatch.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
        }
      }, 0);
    }
  }

  selectEmpleo(empleoNombre: string) {
    this.registroForm.patchValue({ Empleo: empleoNombre });
    this.hideEmpleoDropdown();
  }

  filterEmpleos(event: Event) {
    const query = (event.target as HTMLInputElement).value;
    this.empleosFiltrado = this.empleos.filter(empleo =>
      empleo.toLowerCase().startsWith(query.toLowerCase())
    );

    if (this.empleosFiltrado.length == 1) {
      setTimeout(() => {
        if (query === this.empleosFiltrado[0]) {
          this.hideEmpleoDropdown();
        }
      }, 0);
    } else if (this.empleosFiltrado.length > 1) {
      setTimeout(() => {
        const firstMatch = this.empleoItems.first;
        if (firstMatch) {
          firstMatch.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
        }
      }, 0);
    }
  }

  mayorDeEdadValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      const today = new Date();
      const birthDate = new Date(control.value);
      let age = today.getFullYear() - birthDate.getFullYear();
      const monthDiff = today.getMonth() - birthDate.getMonth();

      if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
        age--;
      }

      if (age < 18) {
        return of({ menorDeEdad: true }).pipe(delay(1000));
      } else {
        return of(null).pipe(delay(1000));
      }
    };
  }
  closeErrorModal() {
    this.errorMessage = null;
  }
}
