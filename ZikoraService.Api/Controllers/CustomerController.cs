using Microsoft.AspNetCore.Mvc;
using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;

namespace ZikoraService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CustomerDto dto)
        {
            var result = await _customerService.CreateCustomerAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var result = await _customerService.LoginAsync(login);
            return Ok(result);
        }
    }
}
