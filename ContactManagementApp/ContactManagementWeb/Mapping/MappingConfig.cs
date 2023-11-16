using AutoMapper;
using ContactManagementWeb.Models;

namespace ContactManagementWeb.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ContactDTO, ContactCreateDTO>().ReverseMap();
            CreateMap<ContactDTO, ContactUpdateDTO>().ReverseMap();
        }
    }
}
