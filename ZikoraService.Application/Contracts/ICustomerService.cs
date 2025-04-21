using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateCustomerAsync(CustomerDto customerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
