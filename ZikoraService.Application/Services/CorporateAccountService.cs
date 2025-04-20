using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;
using ZikoraService.Infrastructure.Http;

namespace ZikoraService.Application.Services
{
    public class CorporateAccountService : ICorporateAccountService
    {
        private readonly IRemoteHttpClient _httpClient;

        public CorporateAccountService(IRemoteHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateCorporateAccountAsync(CorporateAccountDto account)
        {
            //var response = await _httpClient.PostJSON("auth/create-corp-account", account);
            // return response;
            return null;
        }
    }
}
