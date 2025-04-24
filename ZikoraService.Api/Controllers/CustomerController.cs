using Microsoft.AspNetCore.Mvc;
using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;

namespace ZikoraService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ExecutionResult<>), 400)]

    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IOrganizationService _organizationService;
        private readonly ICorporateAccountService _corporateAccountService;



        public CustomerController(ICustomerService customerService, IOrganizationService organizationService, ICorporateAccountService corporateAccountService)
        {
            _customerService = customerService;
            _organizationService = organizationService;
            _corporateAccountService = corporateAccountService;
        }

        [ProducesResponseType(typeof(ExecutionResult<CustomerResponse>), 200)]
        [HttpPost("create-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto dto)
        {
            var result = CustomResponse<CustomerResponse>(await _customerService.CreateCustomerAsync(dto).ConfigureAwait(false));
            return result;
        }

        [ProducesResponseType(typeof(ExecutionResult<AuthResponse>), 200)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
           
            var result = CustomResponse<AuthResponse>(await _customerService.LoginAsync(login).ConfigureAwait(false));
            return result;
        }

        [ProducesResponseType(typeof(ExecutionResult<OrganizationResponse>), 200)]
        [HttpPost("create-org-customer")]
        public async Task<IActionResult> CreateOrganizationCustomer([FromBody] OrganizationDto dto)
        {
            var result = await _organizationService.CreateOrganizationAsync(dto);
            return CustomResponse(result);
        }

        [ProducesResponseType(typeof(ExecutionResult<CorporateResponse>), 200)]
        [HttpPost("create-corporate-account")]
        public async Task<IActionResult> CreateCorporateAccount([FromBody] CorporateAccountDto dto)
        {
            var result = await _corporateAccountService.CreateCorporateAccountAsync(dto);
            return CustomResponse(result);
        }
    }
}
