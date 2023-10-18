using AutoMapper;
using Data.Models;

namespace Web_API.Mappings
{

    internal class SchoolClassProfile : Profile
    {
        public SchoolClassProfile()
        {
            CreateMap<SchoolClass, SchoolClassDTO>().ReverseMap();
        }
    }
}
