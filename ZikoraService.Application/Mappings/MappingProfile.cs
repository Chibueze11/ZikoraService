using AutoMapper;
using ZikoraService.Application.Dtos;
using ZikoraService.Domain.Entities;

namespace ZikoraService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<OrganizationDto, Organization>().ReverseMap();
            CreateMap<CorporateAccountDto, CorporateAccount>().ReverseMap();
        }
    }
}
