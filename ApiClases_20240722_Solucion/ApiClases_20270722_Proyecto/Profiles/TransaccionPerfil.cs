namespace ApiClases_20270722_Proyecto.Profiles;

public class TransaccionPerfil: Profile{

    public TransaccionPerfil()
    {
        CreateMap<Entidades.Transaccion, Modelos.TransaccionDto>();
        CreateMap<Modelos.TransaccionDto, Entidades.Transaccion>();
    }
}
