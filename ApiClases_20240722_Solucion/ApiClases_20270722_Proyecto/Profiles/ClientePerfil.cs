namespace ApiClases_20270722_Proyecto.Profiles;

public class ClientePerfil:Profile {

    public ClientePerfil(){
        CreateMap<Entidades.Cliente, Modelos.ClienteDto>();
        CreateMap<Modelos.ClienteDto, Entidades.Cliente>();
        CreateMap<Entidades.Cliente, Grupo1.ClienteDtoGrupo1>();
        CreateMap<Grupo1.ClienteDtoGrupo1, Entidades.Cliente>();
        CreateMap<Grupo1.ClienteDtoGrupo1, Modelos.ClienteDto>();
        CreateMap<Modelos.ClienteDto, Grupo1.ClienteDtoGrupo1>();



        //CreateMap<Entidades.Cliente, Grupo1.ClienteDtoGrupo1>().ReverseMap();
        //CreateMap<Entidades.Cliente, Modelos.ClienteDto>().ReverseMap();
        //CreateMap<Grupo1.ClienteDtoGrupo1, Modelos.ClienteDto>().ReverseMap();


    }



}
