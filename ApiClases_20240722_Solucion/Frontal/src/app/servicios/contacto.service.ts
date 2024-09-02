import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { Contacto } from '../interfaces/contactos';

@Injectable({
  providedIn: 'root'
})
export class ContactoService{
  
  private url = 'https://localhost:7138/api/VContactos';
  constructor(private http: HttpClient) { }
  getListaContactosPorToken(token: string): Observable<Contacto[]>{
    console.log(token);
    return this.http.get<Contacto[]>(`${this.url}/${token}`).pipe(
      tap(data => data),
      catchError(this.handleError)
    );
  }

  getListaContactosPorId(id: number): Observable<Contacto[]> {
    return this.http.get<Contacto[]>(`${this.url}/getAllContactsByToken/${id}`).pipe(
      tap(data => data),
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

}

