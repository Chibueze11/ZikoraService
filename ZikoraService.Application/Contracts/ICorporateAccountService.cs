using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Contracts
{
    public interface ICorporateAccountService
    {
        Task<ExecutionResult<CorporateResponse>> CreateCorporateAccountAsync(CorporateAccountDto account);
    }
}
