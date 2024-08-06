import { Injectable } from "@angular/core";
import { ICliente } from "./cliente";
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, tap, throwError } from "rxjs";

@Injectable({
    providedIn: 'any'
})
export class ClienteServicio{
    constructor(private http: HttpClient) {} // Paso 2: Inyecta HttpClient

    private url = 'https://localhost:7138/api/Clientes'; // Paso 3: Define la URL del archivo JSON

  obtenerProductos(): Observable<ICliente[]> {

    return this.http.get<ICliente[]>(this.url).pipe(
         tap(data => console.log('Todos: ', JSON.stringify(data))),
         catchError(this.handleError)
    ); // Paso 4: Realiza la petición HTTP
    
  }

 
  private handleError(err: HttpErrorResponse) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.log("Se ha producido un error: "+errorMessage);
    return throwError(() => errorMessage);
  }

}