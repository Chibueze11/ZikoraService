using ZikoraService.Application.Dtos;

namespace ZikoraService.Application.Contracts
{
    public interface ICustomerService
    {
        Task<string> CreateCustomerAsync(CustomerDto customerDto);
        Task<string> LoginAsync(string email, string password);
    }
}
