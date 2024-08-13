import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { ICliente } from '../interfaces/cliente';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClienteEstService {

  constructor(private http: HttpClient) {}

  private readonly url_estadistica = environment.apiUrl;

  private readonly url_prueba = 'https://localhost:7144/api/usuarios';

  getClientes(): Observable<ICliente[]> {
    return this.http.get<ICliente[]>(`${this.url_estadistica}/getclientes`);
  }

  getCliente(clienteId: number): Observable<ICliente> {
    return this.http.get<ICliente>(`${this.url_estadistica}/getclientes/${clienteId}`)
  }

}
