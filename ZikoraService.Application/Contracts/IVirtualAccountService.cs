using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZikoraService.Application.Dtos;
using ZikoraService.Domain.Entities;

namespace ZikoraService.Application.Contracts
{
    public interface IVirtualAccountService
    {
        Task<ExecutionResult<dynamic>> CreateVirtualAccountAsync(CreateVirtualAccountDto dto);
        Task<ExecutionResult<dynamic>> InitiatePaymentAsync(InitiatePaymentDto dto);
        Task<ExecutionResult<dynamic>> VerifyTransactionAsync(VerifyTransactionDto dto);
    }
}
