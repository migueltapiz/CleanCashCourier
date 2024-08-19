export interface ICliente {
  id: number;
  nombre: string;
  apellidos: string;
  usuario: string;
  pais: string;
  fechaNacimiento: Date;
  trabajo?: string;  // Asegúrate de incluir todas las propiedades necesarias
  correo?: string;   // Agrega la propiedad correo aquí
}
asd
