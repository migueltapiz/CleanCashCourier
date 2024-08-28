using ApiClases_20270722_Proyecto.Modelos.Transacciones;

namespace ApiClases_20270722_Proyecto.Profiles;

public class TransaccionPerfil: Profile{

    public TransaccionPerfil()
    {

        // Mapeo de Transaccion a TransaccionBaseDto y viceversa
        CreateMap<Transaccion, TransaccionBaseDto>().ReverseMap();

        // Mapeo de Transaccion a TransaccionGetDto
        CreateMap<Transaccion, TransaccionGetDto>()
            .IncludeBase<Transaccion, TransaccionBaseDto>();

        // Mapeo de TransaccionPostDto a Transaccion
        CreateMap<TransaccionPostDto, Transaccion>()
            .IncludeBase<TransaccionBaseDto, Transaccion>();

        // Mapeo de Transaccion a TransaccionPostDto
        CreateMap<Transaccion, TransaccionPostDto>()
            .IncludeBase<Transaccion, TransaccionBaseDto>();


    }
}
