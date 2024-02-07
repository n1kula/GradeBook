using AutoMapper;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Entities;

namespace Gradebook.Application.Configuration.Mappings
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Age, conf => conf.MapFrom(model => DateTime.Now.Year - model.DOB.ToDateTime(TimeOnly.Parse("00:00")).Year));
        }
    }
}
