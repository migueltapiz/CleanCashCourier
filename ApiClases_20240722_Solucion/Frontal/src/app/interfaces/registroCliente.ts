import { Timestamp } from "rxjs";

export interface RegistroCliente {
  Nombre: string;
  Apellido: string;
  Email: string;
  Contrasena: string;
  PaisId: number;
  Empleo: string;
  FechaNacimiento: Date;
}
export interface InicioSesionCliente {
  Usuario: string;
  Contrasena: string;
  Recuerdame: boolean;
}
