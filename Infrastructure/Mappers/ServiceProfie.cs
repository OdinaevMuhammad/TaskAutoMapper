namespace Infrastructure.Mappers;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<AddStudent,Student>()
        .ForMember(dest => dest.profileImage, opt => opt.MapFrom(src => src.profileImage.FileName));
        CreateMap<AddEnrollment , Enrollment>();
    }
}