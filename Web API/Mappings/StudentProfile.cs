using AutoMapper;
using Data.Models;

namespace Web_API.Mappings
{
    internal class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<SchoolClass, Student>()
                .ForMember(dest => dest.SchoolClass, act => act.MapFrom(src => src));
        }
    }
}
