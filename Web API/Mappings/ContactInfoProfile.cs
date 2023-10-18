using AutoMapper;
using Data.Models;

namespace Web_API.Mappings
{
    internal class ContactInfoProfile : Profile
    {
        public ContactInfoProfile()
        {
            CreateMap<ContactInfo, ContactInfoDTO>().ReverseMap();

            CreateMap<Teacher, ContactInfo>()
                .ForMember(dest => dest.Teacher, act => act.MapFrom(src => src));
        }
    }
}
