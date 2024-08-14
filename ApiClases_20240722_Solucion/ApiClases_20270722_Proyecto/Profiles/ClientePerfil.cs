namespace ApiClases_20270722_Proyecto.Profiles;

public class ClientePerfil:Profile {

    public ClientePerfil(){
        CreateMap<Cliente, ClienteDto>().ReverseMap();
        CreateMap<Pais, PaisDto>().ReverseMap();
    }



}
