namespace ApiClases_20270722_Proyecto.Profiles
{
    public class PaisProfile : Profile
    {
        public PaisProfile()
        {
            CreateMap<Pais, PaisDto>().ReverseMap();
        }
    }
}
