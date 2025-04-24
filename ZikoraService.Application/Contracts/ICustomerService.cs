using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Contracts
{
    public interface ICustomerService
    {
        Task<ExecutionResult<CustomerResponse>> CreateCustomerAsync(CustomerDto customerDto);
        Task<ExecutionResult<AuthResponse>> LoginAsync(LoginDto loginDto);
    }
}
