﻿

namespace ApiClases_20270722_Proyecto.Repositorios;

public class TransaccionRepositorioBBDD : ITransaccionRepositorio
{
    private readonly Contexto _contexto;

    public TransaccionRepositorioBBDD(Contexto contexto) {
        _contexto = contexto;
    }
    public void Actualizar(int id, Transaccion transaccion) {
        _contexto.Transacciones.Update(transaccion);
    }
    public void Agregar(Transaccion transaccion) {
        _contexto.Transacciones.Add(transaccion);
    }
    public void Borrar(int id) {
        var transaccion = _contexto.Transacciones.FirstOrDefault(c => c.Id == id);
        _contexto.Transacciones.Remove(transaccion);
    }
    public Transaccion ObtenerTransaccionId(int id) {
        return _contexto.Transacciones.FirstOrDefault(c => c.Id == id);
    }
  
    public async Task<bool> GuardarCambios() {

        return await _contexto.SaveChangesAsync() > 0;

    }

    public Task<List<Transaccion>> ObtenerTodosFiltrado(DateTime? fechaInicio, DateTime? fechaFin, double? cantidadEnviadaMin, double? cantidadEnviadaMax, double? cantidadRecibidaMin, double? cantidadRecibidaMax) {

        var consulta = _contexto.Transacciones.Select(transaccion => transaccion);

        if(fechaInicio != null){
            consulta = consulta.Where(transaccion => fechaInicio <= transaccion.Fecha);
        }
        if(fechaFin != null)
        {
            consulta = consulta.Where(transaccion => transaccion.Fecha <= fechaFin);
        }
        if(cantidadEnviadaMin != null) {
            consulta = consulta.Where(transaccion => cantidadEnviadaMin <= transaccion.CantidadEnvia);
        }
        if(cantidadEnviadaMax != null)
        {
            consulta = consulta.Where(transaccion => cantidadEnviadaMax >= transaccion.CantidadEnvia);
        }
        if(cantidadRecibidaMin != null)
        {
            consulta = consulta.Where(transaccion => cantidadRecibidaMin <= transaccion.CantidadRecibe);
        }
        if(cantidadRecibidaMax != null)
        {
            consulta = consulta.Where(transaccion => cantidadRecibidaMax >= transaccion.CantidadRecibe);
        }

        return consulta.ToListAsync(); 
    }
}
