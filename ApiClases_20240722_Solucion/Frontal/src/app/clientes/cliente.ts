export interface ICliente {
  
  
  id: number;
  nombre: string;
  apellido: string;
  usuario: string;
  nombrePais: string;
  fechaNacimiento: Date;
  paisId: number;
  trabajo: string;  // Asegúrate de incluir todas las propiedades necesarias
  email: string;   // Agrega la propiedad correo aquí
}
