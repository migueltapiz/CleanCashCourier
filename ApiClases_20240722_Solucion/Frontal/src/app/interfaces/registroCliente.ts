import { Timestamp } from "rxjs";

export interface RegistroCliente {
  Nombre: string;
  Apellido: string;
  Email: string;
  Contrasena: string;
  PaisId: number;
  Empleo: string;
  FechaNacimiento: Date;
  NombrePais: string;
}
export interface InicioSesionCliente {
  Usuario: string;
  Contrasena: string;
  Recuerdame: boolean;
}

export interface ActualizarPerfilCliente {
  Nombre: string;
  Apellido: string;
  Email: string;
  Contrasena: string;
  PaisId: number;
  Empleo: string;
  FechaNacimiento: Date;
  NombrePais: string;
  Id: number;
}
