import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable, catchError, tap, throwError } from "rxjs";
import { ITransaccion, Transaccion } from "./transaccion";

@Injectable({
  providedIn: 'root'
})
export class TransaccionService {
  // private clientesUrl = 'api/clientes/clientes.json';
  private url = 'https://localhost:7138/api/Clientes';

  constructor(private http: HttpClient) { }

  /*getClientes(): Observable<ICliente[]> {
    return this.http.get<ICliente[]>(this.clientesUrl).pipe(
      tap(data => data),
      catchError(this.handleError)
    );
  }*/


  crearTransaccion(transaccion: Transaccion): Observable<Transaccion> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin':'*'});
    const urlConId = `${this.url}/${transaccion.idEnvia}/Transacciones`;
    return this.http.post<Transaccion>(urlConId, transaccion, { headers })
      .pipe(
        tap(data => console.log('Transaccion creada: ', JSON.stringify(data))),
        catchError(this.handleError)
      );
  }
  private handleError(err: HttpErrorResponse) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred (12345678): ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
  getTransacciones(id_cliente:number): Observable<Transaccion[]> {
    return this.http.get<Transaccion[]>(`${this.url}/${id_cliente}/Transacciones`).pipe(
      tap(data => console.log('Transacciones obtenidas: ', JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  getTransaccionesFiltradas(filtros: any,id_cliente:number): Observable<Transaccion[]> {
    let urlConFiltros = `${this.url}/${id_cliente}/Transacciones`;

    const parametros = [];

    if (filtros.fechaInicio) {
      parametros.push(`fechaInicio=${filtros.fechaInicio}`);
    }
    if (filtros.fechaFin) {
      parametros.push(`fechaFin=${filtros.fechaFin}`);
    }
    if (filtros.cantidadMin) {
      parametros.push(`cantidadEnviadaMin=${filtros.cantidadMin}`);
    }
    if (filtros.cantidadMax) {
      parametros.push(`cantidadEnviadaMax=${filtros.cantidadMax}`);
    }

    if (parametros.length > 0) {
      urlConFiltros += '?' + parametros.join('&');
    }

    return this.http.get<Transaccion[]>(urlConFiltros).pipe(
      tap(data => console.log('Transacciones filtradas obtenidas: ', JSON.stringify(data))),
      catchError(this.handleError)
    );
    }
  }
