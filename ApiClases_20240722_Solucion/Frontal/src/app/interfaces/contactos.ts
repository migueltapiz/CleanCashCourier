
export interface FiltroParametrosVistaContacto {
  IdCliente: number;
  NombreUsuarioContacto: string | null;
  Pais: string | null;
  NumeroPaginas: number;
  TamanoPagina: number;
}

export interface Contacto {

  constructor(): void;
  IdCliente: number;
  NombreUsuarioContacto: string;
  Pais: string;
}
