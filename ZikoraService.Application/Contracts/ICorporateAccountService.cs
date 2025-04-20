using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Contracts
{
    public interface ICorporateAccountService
    {
        Task<string> CreateCorporateAccountAsync(CorporateAccountDto account);
    }
}
