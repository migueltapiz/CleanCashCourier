using ApiClases_20270722_Proyecto.Modelos.Clientes;

namespace ApiClases_20270722_Proyecto.Profiles;

public class ClientePerfil:Profile {

    public ClientePerfil(){
        CreateMap<Cliente, ClientePostDto>()
                    .ForMember(dest => dest.NombrePais, opt => opt.MapFrom(src => src.Pais.Nombre));  // Mapea el nombre del país
        CreateMap<ClientePostDto, Cliente>();

        // Mapeo de Cliente a ClienteGetDto y viceversa
        CreateMap<Cliente, ClienteGetDto>()
            .IncludeBase<Cliente, ClientePostDto>(); 

        // Mapeo de ClientePutDto a Cliente
        CreateMap<ClientePutDto, Cliente>()
            .IncludeBase<ClientePostDto, Cliente>();

        // Mapeo de ClienteDeleteDto a Cliente
        CreateMap<ClienteDeleteDto, Cliente>()
            .IncludeBase<ClientePostDto, Cliente>();

        CreateMap<ClienteRegistro, Cliente>().
            IncludeBase<ClientePostDto, Cliente>();


        CreateMap<Pais, PaisDto>().ReverseMap();
    }
}
