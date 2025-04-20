using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Contracts
{
    public interface IOrganizationService
    {
        Task<string> CreateOrganizationAsync(OrganizationDto org);
    }
}
