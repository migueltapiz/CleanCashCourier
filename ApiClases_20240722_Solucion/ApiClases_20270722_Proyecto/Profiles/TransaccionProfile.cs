namespace ApiClases_20270722_Proyecto.Profiles;

public class TransaccionProfile: Profile{

    public TransaccionProfile()
    {
        CreateMap<Entidades.Transaccion, Models.TransaccionDto>();
        CreateMap<Models.TransaccionDto, Entidades.Transaccion>();
    }
}
