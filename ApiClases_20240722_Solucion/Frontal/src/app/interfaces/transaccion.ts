export interface ITransaccion {

  constructor(): void;
  id: number;
  idEnvia: number;
  idRecibe: number;
  cantidadEnvia: number;
  cantidadRecibe: number;
  fecha: string;
}

export class Transaccion implements ITransaccion {

    ["constructor"](): void {
        
    }
    id!: number ;
    idEnvia!: number;
    idRecibe!: number;
    cantidadEnvia!: number;
    cantidadRecibe!: number;
  fecha!: string;
  monedaOrigen!: string;
  monedaDestino!: string;
  costeTransaccion!: number;
}

export interface ITransaccionTabla {

  nombre: string;
  fecha: string;
  cantidad: number;
  tipo: string;

}

export class TransaccionTabla implements ITransaccionTabla {
  ["constructor"](): void {

  }
    nombre!: string;
    fecha!: string;
    cantidad!: number;
    tipo!: string;

}
