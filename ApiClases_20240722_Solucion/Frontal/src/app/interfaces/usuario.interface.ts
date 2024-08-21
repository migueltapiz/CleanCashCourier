import { Timestamp } from "rxjs";

export interface Usuario {
    Email: string;
    Password: string;
    ConfirmPassword: string;
    Nombre: string;
    Apellido: string;
    Rol: string;
    PaisNombre: number;
    Empleo: string;
    FechaNacimiento: Date; // Asegúrate de que FechaNac sea de tipo Date
  }
