using AutoMapper;
using Data.Models;

namespace Web_API.Mappings
{
    internal class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();

            CreateMap<ContactInfo, Teacher>()
                .ForMember(dest => dest.ContactInfo, act => act.MapFrom(src => src));
        }
    }
}
