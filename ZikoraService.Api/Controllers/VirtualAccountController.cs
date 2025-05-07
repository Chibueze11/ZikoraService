using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZikoraService.Application.Contracts;
using ZikoraService.Application.Dtos;
using ZikoraService.Application.Services;
using ZikoraService.Domain.Entities;

namespace ZikoraService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ExecutionResult<>), 400)]

    public class VirtualAccountController : BaseController
    {
        private readonly IVirtualAccountService _virtualAccountService;

        public VirtualAccountController(IVirtualAccountService virtualAccount)
        {
            _virtualAccountService = virtualAccount;
        }

        [ProducesResponseType(typeof(ExecutionResult<dynamic>), 200)]
        [HttpPost("create-account-account")]
        public async Task<IActionResult> CreateVirtualAccount([FromBody] CreateVirtualAccountDto dto)
        {
            var result = CustomResponse<dynamic>(await _virtualAccountService.CreateVirtualAccountAsync(dto).ConfigureAwait(false));
            return result;
        }

        [ProducesResponseType(typeof(ExecutionResult<dynamic>), 200)]
        [HttpPost("initiate-payment")]
        public async Task<IActionResult> CreateCustomer([FromBody] InitiatePaymentDto dto)
        {
            var result = CustomResponse<dynamic>(await _virtualAccountService.InitiatePaymentAsync(dto).ConfigureAwait(false));
            return result;
        }

        [ProducesResponseType(typeof(ExecutionResult<dynamic>), 200)]
        [HttpPost("verify-transaction")]
        public async Task<IActionResult> VerifyTransaction([FromBody] VerifyTransactionDto dto)
        {
            var result = CustomResponse<dynamic>(await _virtualAccountService.VerifyTransactionAsync(dto).ConfigureAwait(false));
            return result;
        }
    }
}
