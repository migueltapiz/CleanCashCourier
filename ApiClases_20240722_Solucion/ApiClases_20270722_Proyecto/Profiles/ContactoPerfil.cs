namespace ApiClases_20270722_Proyecto.Profiles;
public class ContactoPerfil : Profile
{
	public ContactoPerfil()
	{
		CreateMap<Contacto, ContactoDto>().ReverseMap();
	}
}
