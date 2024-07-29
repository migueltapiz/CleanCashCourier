using AutoMapper;
namespace ApiClases_20270722_Proyecto.Profiles;

public class ClienteProfile:Profile {

    public ClienteProfile(){
        CreateMap<Entidades.Cliente,Models.ClienteDto>();
        CreateMap<Models.ClienteDto,Entidades.Cliente>();
        CreateMap<Entidades.Cliente, Grupo1.ClienteDtoGrupo1>();
        CreateMap<Grupo1.ClienteDtoGrupo1, Entidades.Cliente>();
        CreateMap<Grupo1.ClienteDtoGrupo1, Models.ClienteDto>();
        CreateMap<Models.ClienteDto, Grupo1.ClienteDtoGrupo1>();
    }

    

}
