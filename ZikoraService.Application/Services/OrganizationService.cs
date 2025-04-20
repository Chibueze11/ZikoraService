using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;
using ZikoraService.Infrastructure.Http;

namespace ZikoraService.Application.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRemoteHttpClient _httpClient;

        public OrganizationService(IRemoteHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateOrganizationAsync(OrganizationDto org)
        {
     //     var response = await _httpClient.PostAsync("auth/create-org-customer", org);
            return null;
        }
    }
}
