using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateCustomerAsync(CustomerDto customerDto);
        Task<AuthResponse> LoginAsync(LoginDto loginDto);
    }
}
