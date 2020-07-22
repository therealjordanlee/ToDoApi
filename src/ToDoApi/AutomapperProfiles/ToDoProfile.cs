using AutoMapper;
using ToDoApi.Entities;
using ToDoApi.Models;

namespace ToDoApi.AutomapperProfiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDoItem, ToDoModel>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed));
        }
    }
}