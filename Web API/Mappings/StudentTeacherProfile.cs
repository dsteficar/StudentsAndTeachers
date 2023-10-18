using AutoMapper;
using Data.Models;

namespace Web_API.Mappings
{
    internal class StudentTeacherProfile : Profile
    {
        public StudentTeacherProfile()
        {
            CreateMap<StudentTeacher, StudentTeacherDTO>().ReverseMap();
        }
    }
}
