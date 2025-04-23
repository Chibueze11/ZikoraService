using AutoMapper;
using ZikoraService.Application.Dtos;
using ZikoraService.Domain.Entities;

namespace ZikoraService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>()
             .ForMember(dest => dest.DateOfBirth,
                        opt => opt.MapFrom(src => DateOnly.FromDateTime(
                            DateTime.SpecifyKind(src.DateOfBirth.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc))))
             .ReverseMap()
             .ForMember(dest => dest.DateOfBirth,
                        opt => opt.MapFrom(src => src.DateOfBirth));

            CreateMap<OrganizationDto, Organization>().ReverseMap();
            CreateMap<CorporateAccountDto, CorporateAccount>().ReverseMap();
        }
    }
}
