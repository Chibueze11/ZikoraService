using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZikoraService.Domain.Entities
{
    public class InitiatePaymentDto
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
    public class CreateVirtualAccountDto
    {
        public string AccountNumber { get; set; }
        public string ProductCode { get; set; }
        public string NoOfAccounts { get; set; }
        public string TrackingRef { get; set; }
    }
    public class VerifyTransactionDto
    {
        public string UniqueReference { get; set; }
    }
}
