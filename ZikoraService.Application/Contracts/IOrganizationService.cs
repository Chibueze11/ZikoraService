using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Contracts
{
    public interface IOrganizationService
    {
        Task<ExecutionResult<OrganizationResponse>> CreateOrganizationAsync(OrganizationDto org);
    }
}
