namespace ApiClases_20270722_Proyecto.Profiles;

public class ClientePerfil:Profile {

    public ClientePerfil(){
        CreateMap<Cliente, ClienteBaseDto>()
                    .ForMember(dest => dest.NombrePais, opt => opt.MapFrom(src => src.Pais.Nombre))  // Mapea el nombre del país
                    .ReverseMap();
        CreateMap<Pais, PaisDto>().ReverseMap();
    }
}
