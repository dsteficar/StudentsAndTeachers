using AutoMapper;
using Data.Models;
using System.Runtime.InteropServices;

namespace Web_API.Mappings
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
            .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.Name + " " + src.Surname))
            .ForMember(dest => dest.DateOfBirth, act => act.MapFrom(src => new DateTime(src.YearOfBirth,src.MonthOfBirth,src.DayOfBirth,0,0,0)))
            .ReverseMap()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.FullName.Split(' ', StringSplitOptions.None)[0]))
            .ForMember(dest => dest.Surname, act => act.MapFrom(src => src.FullName.Split(' ', StringSplitOptions.None)[1]))
            .ForMember(dest => dest.YearOfBirth, act => act.MapFrom(src => int.Parse(src.DateOfBirth.ToString().Split('.', StringSplitOptions.None)[2])))
            .ForMember(dest => dest.MonthOfBirth, act => act.MapFrom(src => int.Parse(src.DateOfBirth.ToString().Split('.', StringSplitOptions.None)[1])))
            .ForMember(dest => dest.DayOfBirth, act => act.MapFrom(src => int.Parse(src.DateOfBirth.ToString().Split('.', StringSplitOptions.None)[0].Split(' ', StringSplitOptions.None)[0])))
            .ForMember(dest => dest.Age, act => act.MapFrom(src => (int)(DateTime.Now.Year - src.DateOfBirth.Year)))
            .ForMember(dest => dest.IsAdult, act => act.MapFrom(src => (DateTime.Now.Year - src.DateOfBirth.Year) >= 18 ? true : false));

        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
