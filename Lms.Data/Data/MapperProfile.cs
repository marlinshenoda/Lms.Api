

using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Entities;

namespace Lms.Api.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Module, ModuleDto>().ReverseMap();

        }
    }
}
