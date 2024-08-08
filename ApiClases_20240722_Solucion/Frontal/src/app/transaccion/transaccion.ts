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
}
