import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DatosService {

  constructor() { }
  //TODO: Info de países desde la API
  getPaises(): string[] {
  return [
    'Afganistán', 'Albania', 'Alemania', 'Andorra', 'Angola', 'Antigua y Barbuda',
    'Argentina', 'Armenia', 'Australia', 'Austria', 'Azerbaiyán', 'Bahamas',
    'Bahréin', 'Bangladesh', 'Barbados', 'Bélgica', 'Belice', 'Benín', 'Bolivia',
    'Bosnia y Herzegovina', 'Botsuana', 'Brasil', 'Brunei', 'Bulgaria',
    'Burkina Faso', 'Burundi', 'Bután', 'Cabo Verde', 'Camboya', 'Camerún',
    'Canadá', 'Catar', 'Colombia', 'Comoras', 'Congo, República Democrática del',
    'Congo, República del', 'Corea del Norte', 'Corea del Sur', 'Costa Rica',
    'Croacia', 'Cuba', 'Dinamarca', 'Djibouti', 'Dominica', 'Egipto',
    'El Salvador', 'Emiratos Árabes Unidos', 'Eritrea', 'Eslovaquia',
    'Eslovenia', 'España', 'Estados Unidos', 'Esuatini', 'Etiopía', 'Fiyi',
    'Filipinas', 'Finlandia', 'Francia', 'Gabón', 'Gambia', 'Georgia', 'Ghana',
    'Granada', 'Grecia', 'Guatemala', 'Guinea', 'Guinea-Bisáu', 'Guyana', 'Haití',
    'Honduras', 'Hungría', 'India', 'Indonesia', 'Irán', 'Irak', 'Irlanda',
    'Israel', 'Italia', 'Jamaica', 'Japón', 'Jordania', 'Kazajistán', 'Kenia',
    'Kirguistán', 'Kiribati', 'Laos', 'Letonia', 'Líbano', 'Liberia', 'Libia',
    'Liechtenstein', 'Lituania', 'Luxemburgo', 'Madagascar', 'Malasia', 'Malaui',
    'Maldivas', 'Mali', 'Malta', 'Marruecos', 'Mauricio', 'Mauritania', 'México',
    'Micronesia', 'Moldova', 'Mónaco', 'Mongolia', 'Montenegro', 'Mozambique',
    'Myanmar', 'Namibia', 'Nauru', 'Nepal', 'Nicaragua', 'Níger', 'Nigeria',
    'Noruega', 'Nueva Zelanda', 'Omán', 'Pakistán', 'Palaos', 'Panamá',
    'Papúa Nueva Guinea', 'Paraguay', 'Perú', 'Polonia', 'Portugal',
    'Reino Unido', 'República Centroafricana', 'República Dominicana', 'Ruanda',
    'Rumania', 'Rusia', 'San Cristóbal y Nieves', 'San Marino',
    'Santo Tomé y Príncipe', 'Arabia Saudita', 'Senegal', 'Serbia', 'Seychelles',
    'Sierra Leona', 'Singapur', 'Siria', 'Somalia', 'Sri Lanka', 'Sudáfrica',
    'Sudán', 'Suecia', 'Suiza', 'Surinam', 'Taiwán', 'Tanzania', 'Tailandia',
    'Timor Oriental', 'Togo', 'Tonga', 'Trinidad y Tobago', 'Túnez', 'Turquía',
    'Turkmenistán', 'Tuvalu', 'Uganda', 'Ucrania', 'Uruguay', 'Uzbekistán', 'Vanuatu',
    'Venezuela', 'Vietnam', 'Yemen', 'Zambia', 'Zimbabue'
  ];
}


  getTrabajos(): string[] {
    return [
      'Desarrollador de Software', 'Ingeniero Civil', 'Diseñador Gráfico',
      'Médico', 'Abogado', 'Profesor', 'Contador', 'Consultor de Negocios',
      'Arquitecto', 'Marketing Digital'
    ];
  }
}
