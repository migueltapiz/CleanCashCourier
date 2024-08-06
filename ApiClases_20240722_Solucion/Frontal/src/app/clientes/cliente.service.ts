import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable, catchError, tap, throwError } from "rxjs";
import { ICliente } from "./cliente";

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  // private clientesUrl = 'api/clientes/clientes.json';
  private clientesUrl = 'https://localhost:7138/api/Clientes';
  
  constructor(private http: HttpClient) { }
  
  getClientes(): Observable<ICliente[]> {
    return this.http.get<ICliente[]>(this.clientesUrl).pipe(
      tap(data => data),
      catchError(this.handleError)
    );
  }
  private handleError(err: HttpErrorResponse){
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
  

}
