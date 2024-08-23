export class ICliente {
  ["constructor"](): void {

  }
  id!: number;
  nombre!: string;
  apellido!: string;
  usuario!: string;
  pais!: string;
  fechaNacimiento!: Date;
  trabajo?: string;  // Asegúrate de incluir todas las propiedades necesarias
  email?: string;   // Agrega la propiedad correo aquí
}

