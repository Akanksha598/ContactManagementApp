using AutoMapper;
using ContactManagementAPI.Models;
using ContactManagementAPI.Models.Dtos;

namespace ContactManagementAPI.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Contact, ContactDTO>().ReverseMap();
        CreateMap<Contact, ContactCreateDTO>().ReverseMap();
        CreateMap<Contact, ContactUpdateDTO>().ReverseMap();
    }
}
