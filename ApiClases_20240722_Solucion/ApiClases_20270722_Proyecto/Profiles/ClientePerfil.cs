namespace ApiClases_20270722_Proyecto.Profiles;

public class ClientePerfil:Profile {

    public ClientePerfil(){
        CreateMap<Cliente, ClienteBaseDto>()
                    .ForMember(dest => dest.NombrePais, opt => opt.MapFrom(src => src.Pais.Nombre))  // Mapea el nombre del país
                    .ReverseMap();
        CreateMap<Pais, PaisDto>().ReverseMap();

        // Mapeo de Cliente a ClienteGetDto y viceversa
        CreateMap<Cliente, ClienteGetDto>()
            .IncludeBase<Cliente, ClienteBaseDto>(); 

        // Mapeo de ClientePutDto a Cliente
        CreateMap<ClientePutDto, Cliente>()
            .IncludeBase<ClienteBaseDto, Cliente>();

        // Mapeo de ClienteDeleteDto a Cliente
        CreateMap<ClienteDeleteDto, Cliente>()
            .IncludeBase<ClienteBaseDto, Cliente>();

        CreateMap<Cliente, ModeloRegistro>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ReverseMap();
    }
}
